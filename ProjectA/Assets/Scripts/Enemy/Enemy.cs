using System.Linq;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    private Rigidbody rb;
    private Collider cd;
    public CharacterStats stat;
    
    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        cd = GetComponent<Collider>();
        stat = GetComponent<CharacterStats>();
    }

    protected override void Update()
    {
        base.Update();

        
    }

}
