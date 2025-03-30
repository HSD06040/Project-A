using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateController : MonoBehaviour
{
    #region State
    private StateMachine stateMachine;
    public PlayerMoveState moveState {  get; private set; }
    public PlayerIdleState idleState {  get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
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
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void FinishAnimation() => stateMachine.currentState.AnimFinishTrigger();
}
