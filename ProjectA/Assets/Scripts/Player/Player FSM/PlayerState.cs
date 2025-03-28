using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected StateMachine stateMachine;

    protected string animName;

    protected bool timerUse = false;
    protected float stateTimer;
    protected bool finishTrigger;

    protected Vector3 moveDir;

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
        if(timerUse)
            stateTimer -= Time.deltaTime;

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animName, false);
    }

    public void AnimFinishTrigger() => finishTrigger = true;
}
