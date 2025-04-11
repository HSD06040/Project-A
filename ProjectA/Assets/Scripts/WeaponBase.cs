using UnityEditor.AssetImporters;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    private Player player;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        player          = GetComponentInParent<Player>();
        meshFilter      = GetComponent<MeshFilter>();
        meshRenderer    = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.CompareTag("Enemy"))
        {
            CharacterStats stat = target.GetComponent<CharacterStats>();
            player.stat.DoDamage(stat, 1 + (player.stateCon.attackState.comboCount == 0 ? 0 : player.stateCon.attackState.comboCount / 5));
        }
    }

    public void SetupWeaponData(ItemData_Equipment data)
    {
        meshFilter.mesh         = data.itemMesh;
        meshRenderer.material   = data.itemMaterial;
    }
}
