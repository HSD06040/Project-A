using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatStatCalculator
{
    //Èû
    private static int strDamage = 2;
    private static int strMaxHealth = 4;
            
    //¹ÎÃ¸   
    private static int agiDamage = 4;
    private static int agiCritDamage = 2;

    //È°·Â
    private static int vitMaxHealth = 6;
    private static int vitDefense = 1;

    //Çà¿î
    private static int luckCritChance = 1;

    public static void CalculateTotalDamage(CharacterStatController myStatCon, CharacterStatController enemyStatCon, float attackPower)
    {
        bool isCrit = false;
        float totalDamage;

        CharacterStats myStat = myStatCon.stat;

        totalDamage = GetDamage(myStat) * attackPower;

        if (CanCrit(myStat))
        {
            isCrit = true;
            totalDamage *= (GetCritDamage(myStat) / 100);
        }
        totalDamage = CheckTargetDefense(totalDamage, enemyStatCon.stat);

        enemyStatCon.DecreaseHealth((int)totalDamage,isCrit);
    }

    public static bool CanCrit(CharacterStats myStats)
    {
        if (GetCritChance(myStats) > UnityEngine.Random.Range(0, 100))
        {
            return true;
        }
        return false;
    }

    public static float CheckTargetDefense(float totalDamage, CharacterStats enemyStats)
    {
        totalDamage -= totalDamage * (GetDefense(enemyStats) / (GetDefense(enemyStats) + 50));

        return totalDamage;
    }

    #region Get Stats
    public static int GetDamage(CharacterStats Stats)
    {
        return Stats.damage.GetValue() +
              (Stats.strength.GetValue() * strDamage + Stats.agility.GetValue() * agiDamage);
    }

    public static float GetDefense(CharacterStats stats)
    {
        return stats.defense.GetValue() +
               stats.vitality.GetValue() * vitDefense;
    }

    public static int GetMaxHealth(int maxHealth, int vitality, int strength)
    {
        return maxHealth + (vitality * vitMaxHealth) + (strength * strMaxHealth);
    }

    public static int GetCritChance(CharacterStats stats)
    {
        return stats.critChance.GetValue() +
               stats.luck.GetValue() * luckCritChance;
    }

    public static int GetCritDamage(CharacterStats stats)
    {
        return stats.critDamage.GetValue() +
               stats.agility.GetValue() * agiCritDamage;
    }
    #endregion
}
