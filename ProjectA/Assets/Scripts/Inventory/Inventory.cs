using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Inventory
{
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public void AddToInventory(ItemData _data)
    {

    }
}
