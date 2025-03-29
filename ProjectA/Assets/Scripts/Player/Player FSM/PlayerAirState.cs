using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ySpeed = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Gravity();

        if (player.IsGround())
            stateMachine.ChangeState(player.stateCon.idleState);
    }

    public override void Gravity()
    {
        float gravityScale = 0.8f;

        ySpeed += Physics.gravity.y * gravityScale * Time.deltaTime;

        Vector3 move = player.moveDir * player.moveSpeed * Time.deltaTime;

        move.y = ySpeed * Time.deltaTime;

        player.charCon.Move(move);
    }
}
