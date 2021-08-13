using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnSystem : MonoBehaviour
{
    private EnemySpawnData enemySpawnData;
    private readonly HashSet<GameObject> spawnedEnemies = new HashSet<GameObject>();

    private void Awake()
    {
        enemySpawnData = GetComponent<EnemySpawnData>();
        if(enemySpawnData == null) return;
        for (var i = 0; i < enemySpawnData.EnemyAmount; i++) SpawnSingleEnemy();
    }

    public bool CheckIfEnemyRegistered(GameObject enemyGameObject) => spawnedEnemies.Contains(enemyGameObject);

    public void ReInstatiateEnemies(EnemySpawnData newEnemySpawnData)
    {
        enemySpawnData = newEnemySpawnData;
        for (var i = 0; i < enemySpawnData.EnemyAmount; i++) SpawnSingleEnemy();
    }

    public int GetKillableEnemiesAmount()
    {
        if(enemySpawnData == null)
            enemySpawnData = GetComponent<EnemySpawnData>();
        var enemy = enemySpawnData.EnemyPrefab.GetComponent<KillableEnemy>();
        if (enemy == null) return 0;
        return enemy.Health * enemySpawnData.EnemyAmount;
    }

    private void SpawnSingleEnemy()
    {
        var position = enemySpawnData.Area.GetRandomPositionInArea();
        var count = 0;
        while (Physics2D.Raycast(position, position, 1f))
        {
            if (count >= 100)
                return;
            count++;
            position = enemySpawnData.Area.GetRandomPositionInArea();
        }
        spawnedEnemies.Add(
            Instantiate(enemySpawnData.EnemyPrefab, position, Quaternion.identity)
            );
    }

    public void ChangeEnemiesScale(float multiplier, UnityAction<float, Transform> scaler)
    {
        foreach (var enemy in spawnedEnemies)
        {
            if(enemy == null) continue;
            scaler(multiplier, enemy.transform);
        }
    }
}