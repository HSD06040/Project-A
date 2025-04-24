using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static AudioManager audioManager;
    private static UI_Manager uiManager;
    private static DataManager dataManager;
    private static PoolManager poolManager;

    #region Managers
    public static AudioManager Audio                { get { return audioManager; } }
    public static GameManager Instance              { get { return instance; } }
    public static UI_Manager UI                     { get { return uiManager; } }
    public static DataManager Data                  { get { return dataManager; } }
    public static PoolManager Pool                  { get { return poolManager; } }
    #endregion

    private GameManager() { }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        CreateManagers();
        DontDestroyOnLoad(gameObject);
    }
    
    private static T CreateManager<T>() where T : MonoBehaviour
    {
        GameObject go = new GameObject(typeof(T).Name);

        if (instance != null)
            go.transform.parent = instance.transform;

        T manager = go.AddComponent<T>();

        return manager;
    }

    private void CreateManagers()
    {
        uiManager           = CreateManager<UI_Manager>();
        dataManager         = CreateManager<DataManager>();
        audioManager        = CreateManager<AudioManager>();
        poolManager         = CreateManager<PoolManager>();
    }
}
