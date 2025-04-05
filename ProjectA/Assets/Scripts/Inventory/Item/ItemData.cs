using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType 
{   
    Use,
    Equipment 
}

[CreateAssetMenu(fileName = "newItemData",menuName ="Data/ItemData")]
public class ItemData : ScriptableObject, IEquatable<ItemData>
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

    public ItemType itemType;
    public ItemRarity itemGrade;
    public string itemID;
    public Sprite icon;
    public Mesh itemMesh;
    public Material itemMaterial;

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
        itemID = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetStatDescription()
    {
        return "";
    }

    public override bool Equals(object obj)
    {
        return obj is ItemData other && Equals(other);
    }
    public bool Equals(ItemData other)
    {
        if (other == null) return false;
        return itemID == other.itemID;
    }

    public override int GetHashCode()
    {
        return itemID != null ? itemID.GetHashCode() : 0;
    }
}
