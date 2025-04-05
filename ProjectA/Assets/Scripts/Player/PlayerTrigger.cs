using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private PlayerStats stat;

    private void Start()
    {
        stat = GetComponent<PlayerStats>();
    }
}
