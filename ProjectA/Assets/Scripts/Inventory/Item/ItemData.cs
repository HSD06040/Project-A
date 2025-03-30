using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;


public class ItemData : ScriptableObject
{
    public string itemName;
    public enum ItemRarity
    {
        Common,
        UnCommon,
        Rare,
        Epic,
        Unique,
        Legendary
    }

    public ItemRarity itemGrade;
    public string itemId;
    public Sprite icon;

    [TextArea]
    public string itemDescription;

    private static Dictionary<ItemRarity, Color> gradeColors = new Dictionary<ItemRarity, Color>
    {
        {ItemRarity.Common, Color.gray },
        {ItemRarity.UnCommon, new Color(0.5f, 1f, 0.5f) }, // 연한 초록
        {ItemRarity.Rare, Color.blue },
        {ItemRarity.Epic, Color.magenta },
        {ItemRarity.Unique, Color.yellow },
        {ItemRarity.Legendary, new Color(1f, 0.5f, 0f) } // 주황
    };

    public Color GetGradeColor()
    {
        return gradeColors.TryGetValue(this.itemGrade, out Color color) ? color : Color.white; 
    }

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
