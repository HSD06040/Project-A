using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inv;
    public ItemSlot[] inventorySlot;
    public Equipment_Slot[] equipmentSlot;

    private void Awake()
    {
        FindItemSlots();

        inv = new Inventory();

        inv.inventory           = new List<InventoryItem>(inventorySlot.Length);
        inv.inventoryDictionary = new Dictionary<ItemData, InventoryItem> (inventorySlot.Length);
        inv.equipment           = new List<InventoryItem> (equipmentSlot.Length);
        inv.equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>(equipmentSlot.Length);

        UpdateSlotUI();
    }

    public void FindItemSlots()
    {
        Transform parent = GameManager.UI.inventoryPanel.gameObject.transform;

        if (parent != null)
        {
            inventorySlot = parent.GetComponentsInChildren<ItemSlot>()
                      .Where(slot => slot.GetType() == typeof(ItemSlot))
                      .ToArray();
            equipmentSlot = parent.GetComponentsInChildren<Equipment_Slot>();
        }
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
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in inv.equipmentDictionary)
            {
                if (item.Key.equipType == equipmentSlot[i].slotType)
                    equipmentSlot[i].UpdateSlot(item.Value);
            }
        }      

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].ClearSlot();
        }
        for (int i = 0; i < inv.inventory.Count; i++)
        {
            inventorySlot[i].UpdateSlot(inv.inventory[i]);
        }

        GameManager.UI.inventoryPanel.status.UpdateStatusUI();
    }
}
