using UnityEngine;

public class EnemySpawnData : MonoBehaviour
{
    public GameObject EnemyPrefab => enemyPrefab;
    public int EnemyAmount => enemyAmount;
    public Area Area => area;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyAmount;
    [SerializeField] private Area area;
}