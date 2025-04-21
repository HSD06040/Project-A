using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum OverlapType { Sphere, Capsule, Box }

[Serializable]
public class SkillOverlap
{
    public OverlapType overlapType;
    public LayerMask target;
    [SerializeField] private float capsuleHeight;
    [SerializeField] private Vector3 boxRatio;

    public Collider[] GetOverlap(Vector3 position, Vector3 distance, float radius, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion rotation)
    {
        return (overlapType) switch
        {
            OverlapType.Sphere  => SphereOverlap    (position + distance, radius),
            OverlapType.Box     => BoxOverlap       (position + distance, radius, rotation),
            OverlapType.Capsule => CapsuleOverlap   (position + distance, capsuleHeight, radius, rotation.eulerAngles),
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

    public void DrawGizmos(Vector3 origin, Vector3 offset, float range, Quaternion rotation)
    {
        Gizmos.color = Color.green;
        Vector3 center = origin + offset;

        switch (overlapType)
        {
            case OverlapType.Sphere:
                Gizmos.DrawWireSphere(center, range);
                break;

            case OverlapType.Box:
                Gizmos.DrawWireCube(center, boxRatio * range);
                break;

            case OverlapType.Capsule:
                float height = capsuleHeight;
                float radius = range;
                Vector3 up = rotation * Vector3.up;

                Vector3 top = center + up * (height * 0.5f - radius);
                Vector3 bottom = center - up * (height * 0.5f - radius);

                Gizmos.DrawWireSphere(top, radius);
                Gizmos.DrawWireSphere(bottom, radius);
                Gizmos.DrawLine(top + rotation * Vector3.left * radius, bottom + rotation * Vector3.left * radius);
                Gizmos.DrawLine(top + rotation * Vector3.right * radius, bottom + rotation * Vector3.right * radius);
                Gizmos.DrawLine(top + rotation * Vector3.forward * radius, bottom + rotation * Vector3.forward * radius);
                Gizmos.DrawLine(top + rotation * Vector3.back * radius, bottom + rotation * Vector3.back * radius);
                break;
        }
    }
}
