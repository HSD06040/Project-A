using System.Linq;
using UnityEngine;

public class SlimeBT : EnemyBT
{
    [SerializeField] private GameObject[] wayPonts;

    public SlimeBT(Enemy enemy) : base(enemy)
    {
    }

    public void SetUp(GameObject[] wayPoints)
    {
        behaviorAgent.SetVariableValue("Patrol", wayPoints.ToList());
        behaviorAgent.SetVariableValue("Speed", enemy.moveSpeed);
    }

    protected override void Start()
    {
        base.Start();
        SetUp(wayPonts);
    }
}
