using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.jumpDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Vector3 move = player.camMoveDir * player.moveSpeed * Time.deltaTime;

        move.y = player.jumpForce * Time.deltaTime;

        player.controller.Move(move);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.stateCon.airState);
    }
}
