using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    enum CheckType { Distance, Trigger }

    public Dictionary<string, bool> interactiveDic = new Dictionary<string, bool>();
    public Action OnSavedScene;
    public Action OnLoadedScene;

    [SerializeField] private Transform player;
    [SerializeField] private CheckType checkType;
    [SerializeField] private float distance;

    private bool isLoaded;

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
        yield return null;

        OnLoadedScene?.Invoke();
    }

    private void UnLoadScene()
    {
        if (isLoaded)
        {
            OnSavedScene?.Invoke();
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }
}