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
    Weapon, LeftRing, RightRing, Artifact
}

[Serializable]
public struct StatModifier
{
    public StatType Type;
    public int value;
}

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item/EquipmentItem")]
public class ItemData_Equipment : ItemData
{
    [Header("Stat")]
    public StatModifier[] statModifiers;

    public EquipmentType equipType;

    private Dictionary<StatType, string> statTypeToString = new()
    {
        { StatType.Damage       ,"���ݷ�" },
        { StatType.CritDamage   ,"ũ��Ƽ�� ������" },
        { StatType.CritChance   ,"ũ��Ƽ�� Ȯ��" },
        { StatType.MaxHealth    ,"�ִ�ü��" },
        { StatType.Defense      ,"����" },
        { StatType.Strength     ,"��" },
        { StatType.Agility      ,"��ø" },
        { StatType.Vitality     ,"Ȱ��" },
        { StatType.Luck         ,"���" }
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
            GameManager.Data.playerStat.GetStat(modifier.Type).AddModifier(modifier.value);
        }
    }
    public void RemoveModifiers()
    {
        foreach (var modifier in statModifiers)
        {
            GameManager.Data.playerStat.GetStat(modifier.Type).RemoveModifier(modifier.value);
        }
    }

    public int GetMaxHealth()
    {
        int maxHealth = 0;
        int vitality  = 0;
        int strength  = 0;
        
        foreach(var modifier in statModifiers)
        {
            if(modifier.Type == StatType.MaxHealth)
                maxHealth = modifier.value;

            if(modifier.Type == StatType.Vitality)
                vitality = modifier.value;

            if(modifier.Type == StatType.Strength)
                strength = modifier.value;
        }

        return CombatStatCalculator.GetMaxHealth(maxHealth, vitality, strength);
    }
}
