using Unity.Behavior;
using UnityEngine;

public class EnemyBT : MonoBehaviour
{
    protected BehaviorGraphAgent behaviorAgent;
    protected EnemyStats stat;
    protected virtual void Start()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        stat = GetComponent<EnemyStats>();
        stat.OnHealthChanged += _ => Invoke(nameof(SetIsAlive), .05f);
    }

    private void OnEnable() => stat.OnHealthChanged += _ => Invoke(nameof(SetIsAlive), .05f);

    private void OnDisable() => stat.OnHealthChanged -= _ => Invoke(nameof(SetIsAlive), .05f);

    private void SetIsAlive() => behaviorAgent.SetVariableValue("isAlive", stat.IsAlive);

}
