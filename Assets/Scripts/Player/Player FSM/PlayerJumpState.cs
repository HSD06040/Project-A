using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private Vector3 jumpMove;

    public PlayerJumpState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.jumpDuration;

        jumpMove = Vector3.zero;

        Vector3 inputDir = player.camMoveDir.normalized;

        if (player.moveDir.sqrMagnitude > 0.01f)
        {
            jumpMove.x = inputDir.x * player.moveSpeed;
            jumpMove.z = inputDir.z * player.moveSpeed;
        }

        jumpMove.y = player.jumpForce;
    }

    public override void Update()
    {
        base.Update();

        jumpMove.y += Physics.gravity.y * Time.deltaTime;

        player.controller.Move(jumpMove * Time.deltaTime);

        if (stateTimer <= 0)
        {
            player.stateCon.airState.SetAirMove(jumpMove);
            stateMachine.ChangeState(player.stateCon.airState);
        }
    }
}