using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipmentManager : MonoBehaviour
{
    /// <summary>
    /// 아이템을 장착하고 아이템의 능력치를 추가하고 인벤토리에서 제거한다.
    /// </summary>
    public void EquipItem(ItemData_Equipment item)
    {
        if (item == null) return;

        InventoryItem newItem = new InventoryItem(item);
        ItemData_Equipment oldItem = null;

        // 현재 장착중인 아이템을 숭회하며 만약 타입이 같다면 oldItem에 저장
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> _item in GameManager.Inventory.inv.equipmentDictionary)
        {
            if (_item.Key.equipType == item.equipType)
            {
                oldItem = _item.Key;
                break;
            }
        }

        // oldItem이 있다면 원래 장착하던 아이템 장착 해재
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
    /// 아이템을 장착해제 하고 능력치를 삭제한 후 아이템을 다시 인벤토리에 추가한다. 
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
