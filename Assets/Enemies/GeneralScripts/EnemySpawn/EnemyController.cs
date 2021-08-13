using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UnityEvent onAllEnemiesKilled;
    private EnemySpawnSystem[] enemySpawners;
    private int killableEnemiesAmount;

    private void Start()
    {
        foreach (var spawner in enemySpawners)
        {
            killableEnemiesAmount += spawner.GetKillableEnemiesAmount();
        }
        if (killableEnemiesAmount == 0)
        {
            Debug.Log("Level without Killable Enemies");
            onAllEnemiesKilled.Invoke();
        }
    }

    public void DecreaseKillableEnemiesAmount(GameObject enemyGameObject)
    {
        var enemyRegistered = false;
        foreach (var spawner in enemySpawners)
        {
            if (!spawner.CheckIfEnemyRegistered(enemyGameObject)) continue;
            enemyRegistered = true;
            break;
        }
        if(!enemyRegistered) return;
        killableEnemiesAmount -= 1;
        if (killableEnemiesAmount == 0) onAllEnemiesKilled.Invoke();
    }

    public void ChangeEnemyScale(float multiplier, UnityAction<float, Transform> scaler)
    {
        if(enemySpawners == null) return; 
        foreach (var enemySpawner in enemySpawners) enemySpawner.ChangeEnemiesScale(multiplier, scaler);
    }

    public void SetLevelSpawners(EnemySpawnSystem[] spawners)
    {
        enemySpawners ??= spawners;
    }
}
