using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerStats playerStatData {  get; private set; }

    private void Awake()
    {
        playerStatData = GameObject.Find("Player").GetComponent<PlayerStats>();
    }


}
