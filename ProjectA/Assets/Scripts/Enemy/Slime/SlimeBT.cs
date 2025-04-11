using System.Linq;
using UnityEngine;

public class SlimeBT : EnemyBT
{
    private Transform target;

    public void SetUp(Transform target,GameObject[] wayPoints)
    {
        this.target = target;

        behaviorAgent.SetVariableValue("Patrol", wayPoints.ToList());
        behaviorAgent.SetVariableValue("Target", this.target.gameObject);
    }

    protected override void Start()
    {
        base.Start();   
    }
}
