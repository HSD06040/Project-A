using Mono.Cecil;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private EnemyDropTable dropTable;

    protected override void Start()
    {
        base.Start();

        dropTable = GetComponent<EnemyDropTable>();
    }

    protected override void Die()
    {
        base.Die();

        for (int i = 0; i < dropTable.dropItems.Length; i++)
        {
            if (dropTable.dropItems[i].dropChance >= Random.Range(0, 100))
            {
                DropItem(dropTable.dropItems[i].dropItemData);
            }
        }
    }

    private void DropItem(ItemData item)
    {
        GameObject go = Resources.Load("Prefabs/ItemObejct") as GameObject;
        GameObject newItem = Instantiate(go, transform.position, Quaternion.identity);
        newItem.GetComponent<ItemObject>().SetupItemObejct(item);
    }
}
