using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : Entity
{
    public CharacterController controller {  get; private set; }
    public PlayerStateController stateCon {  get; private set; }
    public CharacterStats stat { get; private set; }
    public Vector2 input {  get; private set; }
    public Vector3 camMoveDir { get; private set; }

    [Header("Move info")]
    [HideInInspector] public Vector3 moveDir;
    public float dashSpeed;
    public float dashDuration;

    [Header("Roation info")]
    public float rotSpeed;

    [Header("Jump info")]
    public float jumpForce;
    public float jumpDuration;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController>();
        stateCon = GetComponent<PlayerStateController>();
        stat = GetComponent<CharacterStats>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (moveDir.magnitude > 0)
        {
            Rotation();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            stat.DecreaseHealth(10);
    }

    private void Rotation()
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        camMoveDir = camForward * moveDir.z + camRight * moveDir.x;

        Quaternion targetRotation = Quaternion.LookRotation(camMoveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

        //float rotation = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(0, rotation, 0);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
    }

    private void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }
}
