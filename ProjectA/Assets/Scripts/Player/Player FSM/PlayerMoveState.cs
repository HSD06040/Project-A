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

        Gravity();
    }
}
