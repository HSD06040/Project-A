using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int comboCount {  get; private set; }
    private float lastAttackTimer;
    private float resetTimer = 2;

    public PlayerAttackState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCount > 2 || Time.time >= lastAttackTimer + resetTimer)
            comboCount = 0;

        stateTimer = .3f;

        player.anim.SetInteger("ComboCount", comboCount);
    }

    public override void Exit()
    {
        base.Exit();

        comboCount++;
        lastAttackTimer = Time.time;
    }

    public override void Update()
    {
        base.Update();

        Gravity();

        if (finishTrigger)
            stateMachine.ChangeState(player.stateCon.idleState);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.ChangeState(player.stateCon.dashState);

        if (stateTimer < 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.anim.SetBool("Attack",false);
            Debug.Log("1");
            Enter();
        }
    }
}
