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
        if(inventoryDictionary.TryGetValue(_data, out InventoryItem item))
        {
            item.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_data);
            inventoryDictionary.Add(_data, newItem);
            inventory.Add(newItem);
        }
    }
}
