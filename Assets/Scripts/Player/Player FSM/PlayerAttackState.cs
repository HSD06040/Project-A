using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int comboCount {  get; private set; }
    private float lastAttackTime;
    private float resetTime = 2;

    public PlayerAttackState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ApproachClosestEnemy();
        SetupCombo();
    }

    public override void Exit()
    {
        base.Exit();

        comboCount++;
        lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        Gravity();

        if (Input.GetKeyDown(KeyCode.Mouse0) && player.stateCon.canNextAttack)
        {
            comboCount++;
            SetupCombo();
            player.stateCon.canNextAttack = false;
        }

        if (finishTrigger)
            stateMachine.ChangeState(player.stateCon.idleState);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.ChangeState(player.stateCon.dashState);
    }

    private void SetupCombo()
    {
        Debug.Log(comboCount);
        if (comboCount > 2 || Time.time >= lastAttackTime + resetTime)
            comboCount = 0;

        player.anim.SetInteger("ComboCount", comboCount);

        lastAttackTime = Time.time;
    }
}
