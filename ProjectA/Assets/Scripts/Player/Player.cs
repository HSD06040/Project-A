using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : Entity
{
    public CharacterController charCon {  get; private set; }
    public PlayerStateController stateCon {  get; private set; }

    [Header("Move info")]
    public float moveSpeed;

    protected override void Awake()
    {
        base.Awake();
        charCon = GetComponent<CharacterController>();
        stateCon = GetComponent<PlayerStateController>();
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

    }
}
