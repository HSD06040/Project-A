using Unity.VisualScripting;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(CharacterStatController enemyStat, float attackPower = 1);
    public void DoDamage(CharacterStatController enemyStat, float attackPower = 1);
}
