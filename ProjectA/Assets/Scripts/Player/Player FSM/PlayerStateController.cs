using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    public bool isKnockBack = false;
    public bool isHitting = false;

    #region State
    private StateMachine stateMachine;
    public PlayerMoveState moveState {  get; private set; }
    public PlayerIdleState idleState {  get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    public PlayerKnockBackState knockBackState { get; private set; }
    #endregion

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();

        stateMachine = new StateMachine();
        moveState = new PlayerMoveState(player, stateMachine, "Move");
        idleState = new PlayerIdleState(player, stateMachine, "Idle");
        jumpState = new PlayerJumpState(player, stateMachine, "Jump");
        airState = new PlayerAirState(player, stateMachine, "Jump");
        dashState = new PlayerDashState(player, stateMachine, "Dash");
        attackState = new PlayerAttackState(player, stateMachine, "Attack");
        hitState = new PlayerHitState(player, stateMachine, "Hit");
        knockBackState = new PlayerKnockBackState(player, stateMachine, "KnockBack");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void HitPlayer()
    {
        if (isHitting)
            return;

        if (isKnockBack)
            stateMachine.ChangeState(knockBackState);
        else
            stateMachine.ChangeState(hitState);
    }

    public void SetupKnockBackBool(bool isKnockBack,Transform enemy)
    {
        this.isKnockBack = isKnockBack;

        if (this.isKnockBack)
            player.knockBackDir = (transform.position - enemy.position).normalized;

        HitPlayer();
    }

    public void FinishAnimation() => stateMachine.currentState.AnimFinishTrigger();
}
