using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    protected override void Start()
    {
        base.Start();
    }
    public Stat GetStat(StatType type)
    {
        return type switch
        {
            StatType.Damage => damage,
            StatType.CritDamage => critDamage,
            StatType.CritChance => critChance,
            StatType.MaxHealth => maxHealth,
            StatType.Defense => defense,
            StatType.Strength => strength,
            StatType.Agility => agility,
            StatType.Vitality => vitality,
            StatType.Luck => luck,
            _ => throw new ArgumentException("Invalid stat type")
        };
    }
}
