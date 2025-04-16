using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.Core;
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

    [Header("KnockBack info")]
    public float knockBackForce;
    public Vector3 knockBackDir;
    [Space]

    [SerializeField] private LayerMask enemy;
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
    }

    private void Rotation()
    {
        if (stateCon.isHitting)
            return;

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        
        camForward.Normalize();
        camRight.Normalize();

        camMoveDir = (camForward * moveDir.z + camRight * moveDir.x).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(camMoveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

        //float rotation = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(0, rotation, 0);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
    }

    /// <summary>
    /// 가장 가까운 적에게 회전하면서 다가간다.
    /// </summary>
    public void ApproachClosestEnemy()
    {
        Transform enemyTransform = Utils.FindClosestEnemy(transform, 2f, enemy);

        if (enemyTransform == null)
            return;

        StartCoroutine(MoveTowardsEnemy(enemyTransform));
    }

    /// <summary>
    /// 회전하면서 다가가는 코루틴
    /// </summary>
    private IEnumerator MoveTowardsEnemy(Transform enemyTransform)
    {
        while (Vector3.Distance(transform.position, enemyTransform.position) > .7f)
        {
            if (enemyTransform == null) break;

            Vector3 dir = (enemyTransform.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20f);

            controller.Move(dir * Time.deltaTime * 10);

            yield return null;
        }
    }

    private void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
