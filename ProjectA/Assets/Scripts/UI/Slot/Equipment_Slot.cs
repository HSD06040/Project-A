using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment_Slot : ItemSlot
{
    public EquipmentType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.data == null) return;

        GameManager.Equip.UnEquipItem(item.data as ItemData_Equipment);
        ClearSlot();
        toolTip.CloseToolTip();
    }
    public override void ClearSlot()
    {
        base.ClearSlot();

        item = null;
    }
}