using System.Security.Authentication.ExtendedProtection;
using Unity.Behavior;
using UnityEngine;

public class EnemyBT : MonoBehaviour
{
    protected BehaviorGraphAgent behaviorAgent;
    protected EnemyStats stat;
    protected GameObject target;
    protected float currentDistance;
    protected virtual void Start()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        stat = GetComponent<EnemyStats>();
        stat.OnDead += SetIsAlive;
        stat.OnHealthChanged += _ => SetHit();

        behaviorAgent.GetVariable<GameObject>("Target", out BlackboardVariable<GameObject> targetValue);

        target = targetValue;
    }

    private void OnEnable()
    {
        stat.OnDead += SetIsAlive;
        stat.OnHealthChanged += _ => SetHit();
    }
    private void OnDisable()
    {
        stat.OnDead -= SetIsAlive;
        stat.OnHealthChanged -= _ => SetHit();
    }

    private void Update()
    {
        currentDistance = Vector3.Distance(transform.position, target.transform.position);

        if (currentDistance < 4)
        {
            behaviorAgent.SetVariableValue("isTargetDetected", true);
        }
        else
        {
            behaviorAgent.SetVariableValue("isTargetDetected", false);
        }
    }   

    private void SetIsAlive() => behaviorAgent.SetVariableValue("isAlive", stat.IsAlive);
    private void SetHit()
    {   
        if (behaviorAgent.GetVariable<EnemyState>("currentState", out BlackboardVariable<EnemyState> value))
        {
            if (!(value == EnemyState.Attack))
            {
                behaviorAgent.SetVariableValue("currentState", EnemyState.Hit);
            }
        }
    }
}
