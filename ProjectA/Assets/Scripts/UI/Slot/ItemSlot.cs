using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerMoveHandler
{
    [SerializeField] protected UnityEngine.UI.Image icon;
    [SerializeField] private TextMeshProUGUI itemStack;

    public InventoryItem item;
    protected ItemToolTip toolTip;

    private RectTransform rt;

    private void Start()
    {
        toolTip = GameManager.UI.inventoryPanel.itemToolTip;
    }
    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        icon.color = Color.white;

        if(item != null)
        {
            icon.sprite = item.data.icon;

            if (item.stack > 1)
            {
                itemStack.text = item.stack.ToString();
            }
            else itemStack.text = "";
        }
    }

    public void ClearSlot()
    {
        icon.color = Color.clear;
        icon.sprite = null;
        itemStack.text = "";
        item = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null || item.data == null)
            return;

        toolTip.OpenToolTip(item);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.CloseToolTip();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.data == null)
            return;

        if(item.data.itemType == ItemType.Equipment)
        {
            GameManager.Equip.EquipItem(item.data as ItemData_Equipment);
        }
        else
        {
            // TODO : 아이템 사용, 버리기 UI호출
        }

        toolTip.CloseToolTip();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        RectTransform rt = toolTip.GetComponent<RectTransform>();

        float width = rt.rect.width;
        float height = rt.rect.height;

        Vector3 pos = Input.mousePosition;

        Vector3 finalPos = pos;

        if(pos.x + width > Screen.width)
            finalPos.x = pos.x - width;
        else
            finalPos.x = pos.x + width + 60;

        if(pos.y + height > Screen.height)
            finalPos.y = pos.y - height;
        else
            finalPos.y = pos.y + height - 60;

        rt.position = finalPos;
    }
}
