using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerStats playerStat {  get; private set; }
    public Inventory inventory {  get; private set; }
    public Equipment equipment { get; private set; }

    private void Awake()
    {
        CreateStat();
        inventory   = gameObject.AddComponent<Inventory>();
        equipment   = gameObject.AddComponent<Equipment>();
    }

    private void CreateStat()
    {
        GameObject go = new GameObject("PlayerStat");
        playerStat = go.AddComponent<PlayerStats>();
        playerStat.transform.SetParent(transform);
    }
}
