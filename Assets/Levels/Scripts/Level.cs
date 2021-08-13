using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private EnemySpawnSystem[] enemySpawners;
    [SerializeField] private Transform startPosition;

    public Vector2 PlayerStartPosition => startPosition.position;
    public EnemySpawnSystem[] EnemySpawners => enemySpawners;
}