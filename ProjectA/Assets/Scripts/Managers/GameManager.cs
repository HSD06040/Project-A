using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static AudioManager audioManager;

    #region Managers
    public static AudioManager Audio { get => audioManager; }
    #endregion

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if(instance == null)
                {
                    instance = CreateManager<GameManager>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        CreateManagers();
    }

    private static T CreateManager<T>() where T : MonoBehaviour
    {
        GameObject go = new GameObject(typeof(T).Name);
        if (instance != null)
            go.transform.parent = instance.transform;
        T manager = go.AddComponent<T>();
        DontDestroyOnLoad(go);
        return manager;
    }

    public void CreateManagers()
    {
        audioManager = CreateManager<AudioManager>();

    }
}
