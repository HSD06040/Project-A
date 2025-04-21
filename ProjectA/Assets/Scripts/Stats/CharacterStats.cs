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
    public Stat strength;   // ������ + �ִ�ü��
    public Stat agility;    // ũ�������� + ������
    public Stat vitality;   // �ִ�ü�� + ����
    public Stat luck;       // ���Ȯ���� �ΰ� (ũ��Ƽ�� Ȯ�� ����)

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
