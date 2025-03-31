using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStatCalculator : MonoBehaviour
{
    //Èû
    private int strDamage = 2;
    private int strMaxHealth = 4;
            
    //¹ÎÃ¸   
    private int agiDamage = 4;
    private int agiCritDamage = 2;

    //È°·Â
    private int vitMaxHealth = 6;
    private int vitDefense = 1;

    //Çà¿î
    private int luckCritChance = 1;

    private static WeakReference<CombatStatCalculator> instance;

    private CombatStatCalculator() { }

    public static CombatStatCalculator Instacne {
        get
        {
            if(instance == null || instance.TryGetTarget(out CombatStatCalculator singleton))
            {
                singleton = new CombatStatCalculator();
                instance= new WeakReference<CombatStatCalculator>(singleton);
            }

            return singleton;
        } 
    }

    public void CalculateTotalDamage(CharacterStats myStats, CharacterStats enemyStats, float attackPower)
    {
        float totalDamage;

        totalDamage = GetDamage(myStats) * attackPower;

        if (CanCrit(myStats))
        {
            totalDamage *= (GetCritDamage(myStats) / 100);
        }
        totalDamage = CheckTargetDefense(totalDamage, enemyStats);

        enemyStats.DecreaseHealth((int)totalDamage);
    }

    public bool CanCrit(CharacterStats myStats)
    {
        if (GetCritChance(myStats) > UnityEngine.Random.Range(0, 100))
        {
            return true;
        }
        return false;
    }

    public float CheckTargetDefense(float totalDamage, CharacterStats enemyStats)
    {
        totalDamage -= totalDamage * (GetDefense(enemyStats) / (GetDefense(enemyStats) + 50));

        return totalDamage;
    }

    #region Get Stats
    public int GetDamage(CharacterStats Stats)
    {
        return Stats.damage.GetValue() +
              (Stats.strength.GetValue() * strDamage + Stats.agility.GetValue() * agiDamage);
    }

    public float GetDefense(CharacterStats stats)
    {
        return stats.defense.GetValue() +
               stats.vitality.GetValue() * vitDefense;
    }

    public int GetMaxHealth(CharacterStats stats)
    {
        return stats.maxHealth.GetValue() +
               stats.vitality.GetValue() * vitMaxHealth + stats.strength.GetValue() * strMaxHealth;
    }

    public int GetCritChance(CharacterStats stats)
    {
        return stats.critChance.GetValue() +
               stats.luck.GetValue() * luckCritChance;
    }

    public int GetCritDamage(CharacterStats stats)
    {
        return stats.critDamage.GetValue() +
               stats.agility.GetValue() * agiCritDamage;
    }
    #endregion
}
