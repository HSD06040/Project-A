using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Damage,
    CritChance,
    CritDamage,
    MaxHealth,
    Defense,
    Strength,
    Agility,
    Vitality,
    Luck
}
public enum EquipmentType
{
    Weapon, Ring1, Ring2, Ring3
}

[Serializable]
public struct StatModifier
{
    public StatType Type;
    public int value;
}

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/EquipmentData")]
public class ItemData_Equipment : ItemData
{
    [Header("Stat")]
    public StatModifier[] statModifiers;

    public EquipmentType equipType;

    private Dictionary<StatType, string> statTypeToString = new()
    {
        { StatType.Damage       ,"공격력" },
        { StatType.CritDamage   ,"크리티컬 데미지" },
        { StatType.CritChance   ,"크리티컬 확률" },
        { StatType.MaxHealth    ,"최대체력" },
        { StatType.Defense      ,"방어력" },
        { StatType.Strength     ,"힘" },
        { StatType.Agility      ,"민첩" },
        { StatType.Vitality     ,"활력" },
        { StatType.Luck         ,"행운" }
    };

    private int descriptionLength;

    public override string GetStatDescription()
    {
        sb.Clear();

        for (int i = 0; i < statModifiers.Length; i++)
        {
            if (statTypeToString.TryGetValue(statModifiers[i].Type,out string name))
            {
                AddItemStatDescription(statModifiers[i].value,name);
            }
        }

        if(descriptionLength < 5)
        {
            for(int i = 0; i < 5 - descriptionLength; i ++)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    public void AddItemStatDescription(int value, string name)
    {
        if (value < 0)
            return;

        sb.AppendLine($"{name} : +{value}");

        descriptionLength++;
    }

    public void AddModifiers()
    {
        foreach (var modifier in statModifiers)
        {
            GameManager.Data.playerStatData.GetStat(modifier.Type).AddModifier(modifier.value);
        }
    }
    public void RemoveModifiers()
    {
        foreach (var modifier in statModifiers)
        {
            GameManager.Data.playerStatData.GetStat(modifier.Type).RemoveModifier(modifier.value);
        }
    }
}
