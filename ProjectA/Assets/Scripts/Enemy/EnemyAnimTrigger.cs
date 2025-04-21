using UnityEngine;

public class EnemyAnimTrigger : MonoBehaviour
{
    [Header("Attack Trigger")]
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackPower;
    [SerializeField] private Transform attackPosition;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void AttackTrigger()
    {
        Collider[] colliders = Physics.OverlapSphere(attackPosition.position, attackRadius);

        foreach (var hit in colliders)
        {
            if(hit.CompareTag("Player"))
            {
                IDamagable damagable = hit.GetComponent<IDamagable>();
                damagable.TakeDamage(enemy.statCon);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }
}
