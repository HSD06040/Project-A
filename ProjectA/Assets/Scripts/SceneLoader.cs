using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    enum CheckType { Distance, Trigger }

    public Dictionary<string, bool> interactiveDic = new Dictionary<string, bool>();

    [SerializeField] private Transform player;
    [SerializeField] private CheckType checkType;
    [SerializeField] private float distance;

    private bool isLoaded;

    private List<Action> onLoadedSceneActions = new List<Action>();
    private List<Action> onSavedSceneActions = new List<Action>();

    public void RegisterOnLoaded(Action action)
    {
        if (action != null && !onLoadedSceneActions.Contains(action))
            onLoadedSceneActions.Add(action);
    }

    public void UnregisterOnLoaded(Action action)
    {
        if (action != null)
            onLoadedSceneActions.Remove(action);
    }

    public void RegisterOnSaved(Action action)
    {
        if (action != null && !onSavedSceneActions.Contains(action))
            onSavedSceneActions.Add(action);
    }

    public void UnregisterOnSaved(Action action)
    {
        if (action != null)
            onSavedSceneActions.Remove(action);
    }

    private void Start()
    {
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }
    }

    private void Update()
    {
        if (checkType == CheckType.Distance)
            DistanceCheck();

        if (Input.GetKeyDown(KeyCode.Q))
            FInd();
    }

    private void FInd()
    {
        foreach (var c in interactiveDic)
        {
            Debug.Log(c);
        }
    }

    private void DistanceCheck()
    {
        if (Vector3.Distance(player.position, transform.position) < distance)
        {
            LoadScene();
        }
        else
        {
            UnLoadScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            LoadScene();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            UnLoadScene();
    }

    private void LoadScene()
    {
        if (!isLoaded)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            operation.completed += (op) =>
            {
                isLoaded = true;
                StartCoroutine(DelayedOnLoaded());
            };
        }
    }

    private IEnumerator DelayedOnLoaded()
    {
        yield return new WaitForSeconds(1);

        for (int i = onLoadedSceneActions.Count - 1; i >= 0; i--)
        {
            Action act = onLoadedSceneActions[i];
            try
            {
                act?.Invoke();
            }
            catch (MissingReferenceException e)
            {
                Debug.LogWarning($"Removed destroyed listener: {e.Message}");
                onLoadedSceneActions.RemoveAt(i);
            }
        }
    }

    private void UnLoadScene()
    {
        if (isLoaded)
        {
            TriggerOnSaved();
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

    private void TriggerOnSaved()
    {
        for (int i = onSavedSceneActions.Count - 1; i >= 0; i--)
        {
            Action act = onSavedSceneActions[i];
            try
            {
                act?.Invoke();
            }
            catch (MissingReferenceException e)
            {
                Debug.LogWarning($"Removed destroyed saved listener: {e.Message}");
                onSavedSceneActions.RemoveAt(i);
            }
        }
    }
}
