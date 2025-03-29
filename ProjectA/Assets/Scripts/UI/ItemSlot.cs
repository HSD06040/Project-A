using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemStack;

    public InventoryItem item;

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
        else
        {
            icon.color = Color.clear;
            icon.sprite = null;
            itemStack.text = "";
        }
    }
}
