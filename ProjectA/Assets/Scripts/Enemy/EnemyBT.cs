using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using Unity.Behavior;
using UnityEngine;

public class EnemyBT : MonoBehaviour
{
    private Transform target;
    private SpawnerBase spawner;

    protected BehaviorGraphAgent behaviorAgent;
    protected EnemyStats stat;
    protected GameObject targetObj;
    protected float currentDistance;
    protected virtual void Start()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        stat = GetComponent<EnemyStats>();
        stat.OnDead += SetIsAlive;
        stat.OnHealthChanged += _ => SetHit();

        behaviorAgent.GetVariable<GameObject>("Target", out BlackboardVariable<GameObject> targetValue);

        targetObj = targetValue;
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
    public void SetUp(Transform target, GameObject[] wayPoints, SpawnerBase spawner)
    {
        this.spawner = spawner;
        this.target = target;

        behaviorAgent.SetVariableValue("Patrol", wayPoints.ToList());
        behaviorAgent.SetVariableValue("Target", this.target.gameObject);
    }

    private void OnDestroy()
    {
        spawner.MinusCurrentCount();
    }
}
