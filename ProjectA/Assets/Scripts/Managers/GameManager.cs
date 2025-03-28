using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static AudioManager audioManager;

    #region Managers
    public static AudioManager Audio
    {
        get
        {
            if (audioManager == null)
            {
                audioManager = FindObjectOfType<AudioManager>();

                if (audioManager == null)
                {
                    audioManager = CreateManager<AudioManager>();
                }
            }
            return audioManager;
        }
        private set => audioManager = value;
    }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    instance = CreateManager<GameManager>();
                }
            }
            return instance;
        }
        private set => instance = value;
    }
    #endregion

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
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

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // 씬 로딩되기전에 실행
    private static void Initialize()
    {
        _ = Instance;
        _ = Audio;
    }
}
