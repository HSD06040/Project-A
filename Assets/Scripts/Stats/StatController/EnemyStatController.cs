using UnityEngine;

public class EnemyStatController : CharacterStatController
{
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<EnemyStats>();
    }

    private EnemyDropTable dropTable;
    private CapsuleCollider cd;

    protected override void Start()
    {
        dropTable = GetComponent<EnemyDropTable>();
        cd = GetComponent<CapsuleCollider>();

        base.Start();
    }

    protected override void Die()
    {
        base.Die();

        cd.enabled = false;

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