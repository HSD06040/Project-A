using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private Vector3 airMove;
    private float gravityMultiplier = 2f;

    public PlayerAirState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        airMove.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        player.controller.Move(airMove * Time.deltaTime);

        if (player.IsGround())
        {
            stateMachine.ChangeState(player.stateCon.idleState);
        }
    }
    public void SetAirMove(Vector3 jumpDirection)
    {
        airMove = jumpDirection;
    }
}
