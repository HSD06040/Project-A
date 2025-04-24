using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.stateCon.isHitting = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.stateCon.isHitting = false;
    }

    public override void Update()
    {
        base.Update();

        if (finishTrigger)
            stateMachine.ChangeState(player.stateCon.idleState);
    }
}

public class PlayerKnockBackState : PlayerState
{
    bool isKnockBack;
    public PlayerKnockBackState(Player _player, StateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.stateCon.isHitting = true;
        isKnockBack = true;
        stateTimer = 2;

    }

    public override void Exit()
    {
        base.Exit();
        player.stateCon.isHitting = false;

        player.anim.ResetTrigger("KnockBackStay");
        player.anim.ResetTrigger("KnockBackGetUp");
    }

    public override void Update()
    {
        base.Update();
        
        if (stateTimer < 1.7f)
            isKnockBack = false;
        if (stateTimer < 1.2f) 
            player.anim.SetTrigger("KnockBackStay");
        if(stateTimer < .5f)
            player.anim.SetTrigger("KnockBackGetUp");

        if (finishTrigger)
            stateMachine.ChangeState(player.stateCon.idleState);

        if(isKnockBack)
            player.controller.Move(player.knockBackDir * player.knockBackForce * Time.deltaTime);

        player.controller.Move(Vector3.up * -1 * Time.deltaTime);
    }
}
