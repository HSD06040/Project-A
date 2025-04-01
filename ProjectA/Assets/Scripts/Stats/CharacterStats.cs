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

    public event Action<int> OnHealthChanged;

    private void Start()
    {
        CurrentHealth = GameManager.Calculator.GetMaxHealth(this);
    }

    public void DoDamage(CharacterStats enemyStat, float attackPower = 1)
    {
        GameManager.Calculator.CalculateTotalDamage(this, enemyStat, attackPower);
    }

    public void DecreaseHealth(int amount)
    {
        CurrentHealth -= amount;

        if(curHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        
    }
}
