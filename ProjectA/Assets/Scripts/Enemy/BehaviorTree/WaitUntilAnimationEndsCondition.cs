using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Wait Until Animation Ends", story: "[Self] Wait Animation Ends", category: "Conditions", id: "d061ed507396484eb6f6fcbd18baf2a8")]
public partial class WaitUntilAnimationEndsCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    private Animator anim;

    public override bool IsTrue()
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    public override void OnStart()
    {
        anim = Self.Value.GetComponent<Animator>();
    }
}
