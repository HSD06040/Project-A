using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Equipment : MonoBehaviour
{
    private PlayerStats playerStat;

    private void Start()
    {
        playerStat = GameManager.Data.playerStat;
    }
    /// <summary>
    /// 아이템을 장착하고 아이템의 능력치를 추가하고 인벤토리에서 제거한다.
    /// </summary>
    public void EquipItem(ItemData_Equipment item)
    {
        if (item == null) return;

        if (!GameManager.Data.inventory.CanAdd())
            return;

        InventoryItem newItem = new InventoryItem(item);
        ItemData_Equipment oldItem = null;

        // 현재 장착중인 아이템을 숭회하며 만약 타입이 같다면 oldItem에 저장
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> _item in GameManager.Data.inventory.equipmentDictionary)
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

        GameManager.Data.inventory.equipment.Add(newItem);
        GameManager.Data.inventory.equipmentDictionary.Add(item, newItem);
        item.AddModifiers();
        GameManager.Data.inventory.RemoveItem(item);
        FindObjectOfType<WeaponBase>().SetupWeaponData(item);

        playerStat.CurrentHealth += item.GetMaxHealth();
    }


    /// <summary>
    /// 아이템을 장착해제 하고 능력치를 삭제한 후 아이템을 다시 인벤토리에 추가한다. 
    /// </summary>
    public void UnEquipItem(ItemData_Equipment itemToRomove)
    {
        if (itemToRomove == null) return;

        if (!GameManager.Data.inventory.CanAdd())
            return;

        if (GameManager.Data.inventory.equipmentDictionary.TryGetValue(itemToRomove,out InventoryItem value))
        {
            GameManager.Data.inventory.equipment.Remove(value);
            GameManager.Data.inventory.equipmentDictionary.Remove(itemToRomove);
            itemToRomove.RemoveModifiers();
            GameManager.Data.inventory.AddItem(itemToRomove);
            playerStat.CurrentHealth -= itemToRomove.GetMaxHealth();
        }
        
    }
}
