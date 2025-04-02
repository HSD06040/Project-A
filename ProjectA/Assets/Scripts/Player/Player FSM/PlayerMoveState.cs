using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

      
        if(player.moveDir.sqrMagnitude > 0)
        {
            player.controller.Move(player.camMoveDir * player.moveSpeed * Time.deltaTime);
        }
        else
        {
            stateMachine.ChangeState(player.stateCon.idleState);
        }

        if (!player.IsGround())
        {
            Vector3 jumpMove = Vector3.zero;

            Vector3 inputDir = player.camMoveDir.normalized;

            if (player.moveDir.sqrMagnitude > 0.01f)
            {
                jumpMove.x = inputDir.x * player.moveSpeed;
                jumpMove.z = inputDir.z * player.moveSpeed;
            }
            jumpMove.y = player.jumpForce;

            stateMachine.ChangeState(player.stateCon.airState);
            player.stateCon.airState.SetAirMove(jumpMove);
        }

        Gravity();
    }
}
