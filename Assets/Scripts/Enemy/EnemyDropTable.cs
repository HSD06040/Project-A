using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct DropItem
{
    public ItemData dropItemData;
    public float dropChance;
}

public class EnemyDropTable : MonoBehaviour
{
    public DropItem[] dropItems;

}
