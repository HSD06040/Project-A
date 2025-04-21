using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public ItemSlot[] inventorySlot;
    public Equipment_Slot[] equipmentSlot;

    private void Awake()
    {
        FindItemSlots();

        inventory           = new List<InventoryItem>(inventorySlot.Length);
        inventoryDictionary = new Dictionary<ItemData, InventoryItem> (inventorySlot.Length);
        equipment           = new List<InventoryItem> (equipmentSlot.Length);
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>(equipmentSlot.Length);
    }

    private void Start()
    {
        UpdateSlotUI();
    }

    public void AddItem(ItemData _data)
    {
        if(CanAdd())
            AddToInventory(_data);

        UpdateSlotUI();
    }

    public void RemoveItem(ItemData _data)
    {
        if(inventoryDictionary.TryGetValue(_data, out InventoryItem item))
        {
            if(item.stack > 1)
            {
                item.RemoveStack();
            }
            else
            {
                inventory.Remove(item);
                inventoryDictionary.Remove(_data);
            }
        }

        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipType == equipmentSlot[i].slotType)
                    equipmentSlot[i].UpdateSlot(item.Value);
            }
        }      

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].ClearSlot();
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlot[i].UpdateSlot(inventory[i]);
        }

        GameManager.UI.inventoryPanel.status.UpdateStatusUI();
    }

    public bool CanAdd()
    {
        if (inventory.Count >= inventorySlot.Length)
            return false;

        return true;
    }

    private void AddToInventory(ItemData _data)
    {

        if (inventoryDictionary.TryGetValue(_data, out InventoryItem item))
        {
            if (item.data.itemType == ItemType.Equipment)
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
    private void FindItemSlots()
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
}
