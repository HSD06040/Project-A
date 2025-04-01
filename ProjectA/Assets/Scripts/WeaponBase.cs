using UnityEditor.AssetImporters;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider cd)
    {
        if(cd.CompareTag("Enemy"))
        {
            Debug.Log("1");
            CharacterStats stat = cd.GetComponent<CharacterStats>();
            Debug.Log("2");
            player.stat.DoDamage(stat, 1 + (player.stateCon.attackState.comboCount == 0 ? 0 : player.stateCon.attackState.comboCount / 5));
            Debug.Log("3");
        }
    }
}
