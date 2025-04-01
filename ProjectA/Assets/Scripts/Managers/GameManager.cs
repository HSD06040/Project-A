using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static AudioManager audioManager;
    private static InventoryManager inventoryManager;
    private static CombatStatCalculator calculator;

    #region Managers
    public static AudioManager Audio {  get { return audioManager; } }
    public static GameManager Instance { get { return instance; } }
    public static InventoryManager Inventory { get { return inventoryManager; } }
    public static CombatStatCalculator Calculator { get { return calculator; } }
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
        audioManager = CreateManager<AudioManager>();
        inventoryManager = CreateManager<InventoryManager>();
        calculator = CreateManager<CombatStatCalculator>();
    }
}
