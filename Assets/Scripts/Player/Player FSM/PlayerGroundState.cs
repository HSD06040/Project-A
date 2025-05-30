using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
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

        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(player.stateCon.jumpState);
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.IsGround())
            stateMachine.ChangeState(player.stateCon.dashState);
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.stateCon.attackState);
    }
}
