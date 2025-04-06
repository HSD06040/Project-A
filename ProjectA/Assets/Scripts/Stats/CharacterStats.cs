using System;
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
    public Stat strength;   // ������ + �ִ�ü��
    public Stat agility;    // ũ�������� + ������
    public Stat vitality;   // �ִ�ü�� + ����
    public Stat luck;       // ���Ȯ���� �ΰ� (ũ��Ƽ�� Ȯ�� ����)

    public EntityFX fx {  get; private set; }

    private bool isAlive;
    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            OnPlayerDead?.Invoke();
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

    public event Action OnPlayerDead;
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
        IsAlive = true;
    }
}
