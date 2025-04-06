using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipmentManager : MonoBehaviour
{
    /// <summary>
    /// �������� �����ϰ� �������� �ɷ�ġ�� �߰��ϰ� �κ��丮���� �����Ѵ�.
    /// </summary>
    public void EquipItem(ItemData_Equipment item)
    {
        if (item == null) return;

        InventoryItem newItem = new InventoryItem(item);
        ItemData_Equipment oldItem = null;

        // ���� �������� �������� ��ȸ�ϸ� ���� Ÿ���� ���ٸ� oldItem�� ����
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> _item in GameManager.Inventory.inv.equipmentDictionary)
        {
            if (_item.Key.equipType == item.equipType)
            {
                oldItem = _item.Key;
                break;
            }
        }

        // oldItem�� �ִٸ� ���� �����ϴ� ������ ���� ����
        if(oldItem != null)
        {
            UnEquipItem(oldItem);
        }

        GameManager.Inventory.inv.equipment.Add(newItem);
        GameManager.Inventory.inv.equipmentDictionary.Add(item, newItem);
        item.AddModifiers();
        GameManager.Inventory.RemoveItem(item);
        GameManager.Data.playerStatData.IncreaseHealth(item.GetMaxHealth());
    }


    /// <summary>
    /// �������� �������� �ϰ� �ɷ�ġ�� ������ �� �������� �ٽ� �κ��丮�� �߰��Ѵ�. 
    /// </summary>
    public void UnEquipItem(ItemData_Equipment itemToRomove)
    {
        if (itemToRomove == null) return;

        if(GameManager.Inventory.inv.equipmentDictionary.TryGetValue(itemToRomove,out InventoryItem value))
        {
            GameManager.Inventory.inv.equipment.Remove(value);
            GameManager.Inventory.inv.equipmentDictionary.Remove(itemToRomove);
            itemToRomove.RemoveModifiers();
            GameManager.Inventory.AddItem(itemToRomove);
            GameManager.Data.playerStatData.DecreaseHealth(itemToRomove.GetMaxHealth());
        }
        
    }
}
