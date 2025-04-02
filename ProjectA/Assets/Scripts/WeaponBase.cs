using UnityEditor.AssetImporters;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.CompareTag("Enemy"))
        {
            CharacterStats stat = target.GetComponent<CharacterStats>();
            player.stat.DoDamage(stat, 1 + (player.stateCon.attackState.comboCount == 0 ? 0 : player.stateCon.attackState.comboCount / 5));
        }
    }
}
