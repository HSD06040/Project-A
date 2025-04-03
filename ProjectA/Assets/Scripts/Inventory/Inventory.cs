using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.InputSystem;

[Serializable]
public class Inventory
{
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public void AddToInventory(ItemData _data)
    {
        
        if (inventoryDictionary.TryGetValue(_data, out InventoryItem item))
        {
            if (item.stack > 0)
            {
                item.AddStack();
                return;
            }
            else
            {
                InventoryItem newItem = new InventoryItem(_data);
                inventoryDictionary[_data] = newItem;
                inventory.Add(newItem);
            }
        }
    }
}
