using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TargetLook", story: "[Self] LookAt [Target]", category: "Action", id: "db8b265040457670efe2508757b0e386")]
public partial class TargetLookAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    private float rotSpeed = 6;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 dir = (Target.Value.transform.position - Self.Value.transform.position).normalized;

        Quaternion targetRatation = Quaternion.LookRotation(dir);

        Self.Value.transform.rotation = Quaternion.Slerp(Self.Value.transform.rotation, targetRatation, rotSpeed * Time.deltaTime);

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

