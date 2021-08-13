using System.Collections;
using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    [SerializeField] private EnemySpawnData[] enemySpawnDatas;
    [SerializeField] private ParticleSystem teleportParticleSystem;
    [SerializeField] private EnemySpawnSystem enemySpawnSystem;

    private Area bossMoveArea;
    private Coroutine bossCoroutine;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bossMoveArea = GameObject.FindGameObjectWithTag("Level").GetComponent<CircleArea>();
        bossCoroutine = StartCoroutine(BossBehaviour());
    }

    public void StopEverything() => StopCoroutine(bossCoroutine);

    private IEnumerator BossBehaviour()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            teleportParticleSystem.Play();
            audioSource.Play();
            yield return new WaitForSeconds(2.5f);
            SpawnEnemies();
            Teleport();
            yield return new WaitForSeconds(Random.Range(5f, 15f));
        }
    }

    private void Teleport()
    {
        transform.position = bossMoveArea.GetRandomPositionInArea();
    }

    private void SpawnEnemies()
    {
        //1 enemy data - 60%
        //2 enemy datas - 30%
        //3 enemy datas - 10%
        var chance = Random.value;
        if (chance <= 0.6f)
        {
            enemySpawnSystem.ReInstatiateEnemies(enemySpawnDatas[Random.Range(0, enemySpawnDatas.Length)]);
        }
        else if (chance <= 0.9f)
        {
            enemySpawnSystem.ReInstatiateEnemies(enemySpawnDatas[Random.Range(0, enemySpawnDatas.Length)]);
            enemySpawnSystem.ReInstatiateEnemies(enemySpawnDatas[Random.Range(0, enemySpawnDatas.Length)]);
        }
        else if (chance > 0.9f)
        {
            enemySpawnSystem.ReInstatiateEnemies(enemySpawnDatas[Random.Range(0, enemySpawnDatas.Length)]);
            enemySpawnSystem.ReInstatiateEnemies(enemySpawnDatas[Random.Range(0, enemySpawnDatas.Length)]);
            enemySpawnSystem.ReInstatiateEnemies(enemySpawnDatas[Random.Range(0, enemySpawnDatas.Length)]);
        }
    }
}
