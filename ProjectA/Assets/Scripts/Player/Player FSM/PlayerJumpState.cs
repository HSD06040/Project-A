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

        ySpeed = player.jumpForce;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Vector3 move = player.camMoveDir * player.moveSpeed * Time.deltaTime;

        ySpeed += Physics.gravity.y * Time.deltaTime;

        move.y = player.jumpForce * ySpeed * Time.deltaTime;

        player.controller.Move(move);

        if (stateTimer < 0)
        {
            ySpeed = player.jumpForce * 0.5f;
            stateMachine.ChangeState(player.stateCon.airState);
        }
    }
}
