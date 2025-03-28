using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private StateMachine stateMachine;
    public PlayerMoveState move {  get; private set; }
    public PlayerIdleState idle {  get; private set; }

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();

        stateMachine = new StateMachine();
        move = new PlayerMoveState(player, stateMachine, "Move");
        idle = new PlayerIdleState(player, stateMachine, "Idle");
    }

    private void Start()
    {
        stateMachine.Initialize(move);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
