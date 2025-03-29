using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected StateMachine stateMachine;

    protected string animName;
    protected float stateTimer;
    protected bool finishTrigger;

    protected float ySpeed = 0;

    public PlayerState(Player _player, StateMachine _stateMachine, string _animName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animName = _animName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animName, true);
        finishTrigger = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        Debug.Log(this.GetType().Name);

        player.anim.SetFloat("yVelocity", ySpeed);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animName, false);
    }

    public void AnimFinishTrigger() => finishTrigger = true;

    public virtual void Gravity()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (player.IsGround())
            ySpeed = -2;

        player.controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }
}

