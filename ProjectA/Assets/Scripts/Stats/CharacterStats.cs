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

    public EntityFX fx {  get; private set; }

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

    public event Action OnDead;
    public event Action<int> OnHealthChanged;

    protected virtual void Start()
    {
        CurrentHealth = GameManager.Calculator.GetMaxHealth(maxHealth.GetValue(), vitality.GetValue(), strength.GetValue());
        fx = GetComponent<EntityFX>();
    }

    public void DoDamage(CharacterStats enemyStat, float attackPower = 1)
    {
        GameManager.Calculator.CalculateTotalDamage(this, enemyStat, attackPower);
    }

    public void TakeDamage(CharacterStats enemyStat, float attackPower = 1)
    {
        GameManager.Calculator.CalculateTotalDamage(enemyStat, this, attackPower);
    }

    public void DecreaseHealth(int amount,bool isCrit = false)
    {
        CurrentHealth -= amount;

        if(curHP <= 0)
        {
            Die();
        }

        if(amount > 0)
            fx.CreatePopUpText(amount.ToString(),transform.position,isCrit? PopUpType.Crit : PopUpType.Damage);
    }
    public void IncreaseHealth(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > GameManager.Calculator.GetMaxHealth(maxHealth.GetValue(), vitality.GetValue(), strength.GetValue()))
            CurrentHealth = GameManager.Calculator.GetMaxHealth(maxHealth.GetValue(), vitality.GetValue(), strength.GetValue());

        if(amount > 0)
            fx.CreatePopUpText(amount.ToString(), transform.position,PopUpType.Heal);
    }

    protected virtual void Die()
    {
        IsAlive = false;
    }
}
