using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    #region State
    private StateMachine stateMachine;
    public PlayerMoveState moveState {  get; private set; }
    public PlayerIdleState idleState {  get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();

        stateMachine = new StateMachine();
        moveState = new PlayerMoveState(player, stateMachine, "Move");
        idleState = new PlayerIdleState(player, stateMachine, "Idle");
        jumpState = new PlayerJumpState(player, stateMachine, "Jump");
        airState = new PlayerAirState(player, stateMachine, "Air");
        dashState = new PlayerDashState(player, stateMachine, "Dash");
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
