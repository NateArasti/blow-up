using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [Header("ChangeOnLoad")] 
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyController enemyController;

    [Header("Level")]
    [SerializeField] private GameObject[] levels;

    private Level currentLevelData;
    public static int CurrentLevelIndex;

    private void Awake()
    {
        if(levels.Length == 0 || player == null) return;
        if (CurrentLevelIndex >= levels.Length) CurrentLevelIndex = levels.Length - 1;
        if (CurrentLevelIndex < 0) CurrentLevelIndex = 0;
        currentLevelData = Instantiate(levels[CurrentLevelIndex]).GetComponent<Level>();
        player.transform.position = currentLevelData.PlayerStartPosition;
        enemyController.SetLevelSpawners(currentLevelData.EnemySpawners);
    }

    public void PassLevel()
    {
        PlayerPrefs.SetInt($"{CurrentLevelIndex}levelPassed", 1);
    }
}