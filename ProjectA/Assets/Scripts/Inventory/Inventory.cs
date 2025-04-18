using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine.InputSystem;

[Serializable]
public class Inventory
{
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public void AddToInventory(ItemData _data)
    {
        
        if (inventoryDictionary.TryGetValue(_data, out InventoryItem item))
        {
            if(item.data.itemType == ItemType.Equipment)
            {
                InventoryItem newItem = new InventoryItem(_data);
                inventory.Add(newItem);
                return;
            }
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
