using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemGrade;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI equipType;
    [SerializeField] private TextMeshProUGUI itemStack;
    [SerializeField] private TextMeshProUGUI itemStat;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private RectTransform toolTipRect;

    private void Start()
    {
        toolTipRect = GetComponent<RectTransform>();
    }

    public void OpenToolTip(InventoryItem item)
    {
        if (item == null)
            return;

        // Setup Text
        itemName.text        = item.data.itemName;
        itemGrade.text       = item.data.itemGrade.ToString();
        itemType.text        = item.data.itemType.ToString();
        itemStack.text       = item.stack.ToString();
        itemDescription.text = item.data.itemDescription;

        // 장비만 스텟추가
        if (item.data is ItemData_Equipment equipmentData)
        {
            itemStat.text  = equipmentData.GetStatDescription();
            equipType.text = equipmentData.equipType.ToString();
            itemStat.gameObject.SetActive(true);
            equipType.gameObject.SetActive(true);

            itemStack.gameObject.SetActive(false);
        }
        else
        {
            itemStat.gameObject.SetActive(false);
            equipType.gameObject.SetActive(false);
        }

        // Setup Color
        itemName.color = item.data.GetGradeColor();
        itemGrade.color = item.data.GetGradeColor();

        gameObject.SetActive(true);
    }

    public void CloseToolTip() => gameObject.SetActive(false);
}
