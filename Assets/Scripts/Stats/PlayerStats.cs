using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private void Awake()
    {
        InitStat();
    }

    private void InitStat()
    {
        damage = new Stat();
        defense = new Stat();
        critChance = new Stat();
        critDamage = new Stat();
        maxHealth = new Stat();
        strength = new Stat();
        vitality = new Stat();
        agility = new Stat();
        luck = new Stat();
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

    public void ClearStat()
    {
        maxHealth.baseValue = 100;
        damage.baseValue = 5;
        defense.baseValue = 1;

        critDamage.baseValue = 0;
        critChance.baseValue = 0;

        strength.baseValue = 0;
        agility.baseValue = 0;
        vitality.baseValue = 0;
        luck.baseValue = 0;
    }
}
