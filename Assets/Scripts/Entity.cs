using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    #region Physics

    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask ground;

    #endregion

    [Header("Move info")]
    public float moveSpeed;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public bool IsGround() => Physics.Raycast(transform.position, Vector3.down, groundDistance, ground);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundDistance, 0));
    }
}
