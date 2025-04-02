using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
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
                Player player = hit.GetComponent<Player>();
                enemy.stat.DoDamage(player.stat,attackPower);

                player.stateCon.SetupKnockBackBool(1 == enemy.anim.GetInteger("AttackCount"),this.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(attackPosition.position, attackRadius);
    }
}
