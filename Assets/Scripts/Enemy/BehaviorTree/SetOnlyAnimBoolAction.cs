using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetOnlyAnimBool", story: "[Self] Set Only Anim True [AnimBoolName]", category: "Action", id: "9d70ae7a46ad93ec0c9ca600bce8db2f")]
public partial class SetOnlyAnimBoolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<string> AnimBoolName;

    private Animator anim;

    protected override Status OnStart()
    {
        anim = Self.Value.GetComponent<Animator>();
        
        if(anim == null)
            return Status.Running;

        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if(param.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(param.name, false);
            }
        }

        anim.SetBool(AnimBoolName, true);

        return Status.Running;
    }
}

