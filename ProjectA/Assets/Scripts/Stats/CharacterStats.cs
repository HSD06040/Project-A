using System;
using System.Net.Mail;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Offensive Stat")]
    public Stat damage;
    public Stat critChance;
    public Stat critDamage;

    [Header("Defense Stat")]
    public Stat maxHealth;
    public Stat defense;

    [Header("Primary stat")]
    public Stat strength;   // 데미지 + 최대체력
    public Stat agility;    // 크리데미지 + 데미지
    public Stat vitality;   // 최대체력 + 방어력
    public Stat luck;       // 모든확률에 부가 (크리티컬 확률 포함)

    private bool isAlive = true;
    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            OnDead?.Invoke();
        }
    }

    private int curHP;
    public int CurrentHealth 
    {  
       get { return curHP; }
       set
       {
            curHP = value;
            OnHealthChanged?.Invoke(curHP);
       }
    }

    public int maxHP { get { return CombatStatCalculator.GetMaxHealth(maxHealth.GetValue(), vitality.GetValue(), strength.GetValue()); } }

    public event Action OnDead;
    public event Action<int> OnHealthChanged;
}
