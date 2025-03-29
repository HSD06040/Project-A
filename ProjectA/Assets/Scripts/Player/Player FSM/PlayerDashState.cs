using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private Vector3 movedir;

    public PlayerDashState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movedir = player.camMoveDir;

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.controller.Move(movedir * player.moveSpeed * player.dashSpeed * Time.deltaTime);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.stateCon.idleState);
    }
}
