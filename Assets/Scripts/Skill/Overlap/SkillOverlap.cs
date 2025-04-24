using System;
using Unity.VisualScripting;
using UnityEngine;

public enum OverlapType { Sphere, Capsule, Box }

[Serializable]
public class SkillOverlap
{
    public OverlapType overlapType;
    public LayerMask target;
    public float capsuleHeight;
    public Vector3 boxRatio;

    public Collider[] GetOverlap(Transform effectTransform, Vector3 distance, float radius, SkillVector skillVec)
    {
        Vector3 forward = skillVec.forward;
        Vector3 right = skillVec.right;
        Vector3 up = skillVec.up;

        Vector3 position = effectTransform.position + (forward * distance.z) + (right * distance.x) + (up * distance.y);

        return (overlapType) switch
        {
            OverlapType.Sphere  => SphereOverlap    (position, radius),

            OverlapType.Box     => BoxOverlap       (position, radius, effectTransform.rotation),

            OverlapType.Capsule => CapsuleOverlap   (position, capsuleHeight, radius, effectTransform.forward),
            _ => null
        };
    }

    private Collider[] SphereOverlap(Vector3 position, float radius)
    {
        return Physics.OverlapSphere(position, radius, target);
    }

    private Collider[] BoxOverlap(Vector3 center, float size, Quaternion rotation)
    {
        return Physics.OverlapBox(center, boxRatio * size, rotation, target);
    }

    private Collider[] CapsuleOverlap(Vector3 center, float height, float radius, Vector3 direction)
    {
        Vector3 offset = direction.normalized * (height * 0.5f - radius);
        Vector3 point0 = center + offset;
        Vector3 point1 = center - offset;

        return Physics.OverlapCapsule(point0, point1, radius, target);
    }

}
