using UnityEngine;

public class CharacterStatController : MonoBehaviour, IDamagable
{
    public CharacterStats stat;
    [SerializeField] private EntityFX fx;

    protected virtual void Awake()
    {
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Start()
    {
        stat.CurrentHealth = stat.maxHP;
    }

    public void DoDamage(CharacterStatController enemyStat, float attackPower = 1)
    {
        CombatStatCalculator.CalculateTotalDamage(this, enemyStat, attackPower);
    }

    public void TakeDamage(CharacterStatController enemyStat, float attackPower = 1)
    {
        CombatStatCalculator.CalculateTotalDamage(enemyStat, this, attackPower);
    }

    public void DecreaseHealth(int amount, bool isCrit = false)
    {
        stat.CurrentHealth -= amount;

        if (stat.CurrentHealth <= 0)
        {
            Die();
        }

        if (amount > 0)
            fx.CreatePopUpText(amount.ToString(), transform.position, isCrit ? PopUpType.Crit : PopUpType.Damage);
    }

    public void IncreaseHealth(int amount)
    {
        stat.CurrentHealth += amount;

        if (stat.CurrentHealth > stat.maxHP)
            stat.CurrentHealth = stat.maxHP;

        if (amount > 0)
            fx.CreatePopUpText(amount.ToString(), transform.position, PopUpType.Heal);
    }
    protected virtual void Die()
    {
        stat.IsAlive = false;
    }
}
