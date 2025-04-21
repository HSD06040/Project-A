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
    /// �������� �����ϰ� �������� �ɷ�ġ�� �߰��ϰ� �κ��丮���� �����Ѵ�.
    /// </summary>
    public void EquipItem(ItemData_Equipment item)
    {
        if (item == null) return;

        if (!GameManager.Data.inventory.CanAdd())
            return;

        InventoryItem newItem = new InventoryItem(item);
        ItemData_Equipment oldItem = null;

        // ���� �������� �������� ��ȸ�ϸ� ���� Ÿ���� ���ٸ� oldItem�� ����
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> _item in GameManager.Data.inventory.equipmentDictionary)
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

        GameManager.Data.inventory.equipment.Add(newItem);
        GameManager.Data.inventory.equipmentDictionary.Add(item, newItem);
        item.AddModifiers();
        GameManager.Data.inventory.RemoveItem(item);
        FindObjectOfType<WeaponBase>().SetupWeaponData(item);

        playerStat.CurrentHealth += item.GetMaxHealth();
    }


    /// <summary>
    /// �������� �������� �ϰ� �ɷ�ġ�� ������ �� �������� �ٽ� �κ��丮�� �߰��Ѵ�. 
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
