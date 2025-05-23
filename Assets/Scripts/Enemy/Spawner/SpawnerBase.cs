using System;
using System.Threading;
using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    [Serializable]
    struct PatrolPoint
    {
        public GameObject[] patrolPoint;
    }

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform target;
    [SerializeField] private float spawnDuration;
    private float spawnTimer;
    [SerializeField] private int maxEnemyCount;
    public int currentEnemyCount;

    [SerializeField] PatrolPoint[] patrolPoints;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer < 0 && maxEnemyCount > currentEnemyCount)
        {
            InstantiateEnemy();
            spawnTimer = spawnDuration;
        }
    }
    
    private void InstantiateEnemy()
    {
        if (currentEnemyCount >= maxEnemyCount) return;

        currentEnemyCount++;
        GameObject newEnemy = Instantiate(enemy,transform.position,Quaternion.identity);
        newEnemy.GetComponent<EnemyBT>()?.SetUp(target, patrolPoints[currentEnemyCount].patrolPoint,this);
    }
    public void MinusCurrentCount() => currentEnemyCount--;
}
