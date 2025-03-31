using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inv;
    public ItemSlot[] inventorySlot;

    private void Awake()
    {
        FindItemSlots();
        inv = new Inventory();
        inv.inventoryDictionary = new Dictionary<ItemData, InventoryItem> (inventorySlot.Length);
    }

    public void FindItemSlots()
    {
        Transform parent = GameObject.Find("InventoryPanel")?.transform;

        if (parent != null)
            inventorySlot = parent.GetComponentsInChildren<ItemSlot>();
    }

    public void AddItem(ItemData _data)
    {
        inv.AddToInventory(_data);
        UpdateSlotUI();
    }
    public void RemoveItem(ItemData _data)
    {
        if(inv.inventoryDictionary.TryGetValue(_data, out InventoryItem item))
        {
            if(item.stack > 1)
            {
                item.RemoveStack();
            }
            else
            {
                inv.inventory.Remove(item);
                inv.inventoryDictionary.Remove(_data);
            }
        }

        UpdateSlotUI();
    }
    public void UpdateSlotUI()
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].UpdateSlot(inv.inventory[i]);
        }
    }
}
