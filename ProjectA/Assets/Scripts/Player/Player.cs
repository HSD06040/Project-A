using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : Entity
{
    public CharacterController charCon {  get; private set; }
    public PlayerStateController stateCon {  get; private set; }

    [Header("Move info")]
    public float moveSpeed;
    public Vector3 moveDir;
    public float dashSpeed;
    public float dashDuration;


    [Header("Jump info")]
    public float jumpForce;
    public float jumpDuration;

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
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }
}
