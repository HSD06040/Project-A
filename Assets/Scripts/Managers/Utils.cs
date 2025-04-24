using UnityEngine;

public static class Utils
{
    /// <summary>
    /// 각도로 좌표 구하기
    /// </summary>
    public static Vector3 GetPositionFromAngle(float radius, float angle)
    {
        Vector3 position = Vector3.zero;

        angle = Mathf.Deg2Rad * angle;

        position.x = Mathf.Cos(angle) * radius;
        position.z = Mathf.Sin(angle) * radius;

        return position;
    }

    /// <summary>
    /// 가장 가까운 적 구하기
    /// </summary>
    public static Transform FindClosestEnemy(Transform transform,float radius, LayerMask layerMask)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in colliders)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }
}
