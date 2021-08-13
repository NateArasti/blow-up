using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] private int levelCount;
    public static int FirstNotPassedLevel { get; private set; }

    private void Awake()
    {
        for (var i = 0; i < levelCount; i++)
        {
            if (PlayerPrefs.GetInt($"{i}levelPassed", 0) == 1) continue;
            FirstNotPassedLevel = i;
            break;
        }
    }

    public void SetCurrentLevelIndex(int levelIndex)
    {
        LevelSystem.CurrentLevelIndex = levelIndex;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadNextLevel()
    {
        LevelSystem.CurrentLevelIndex += 1;
        SceneManager.LoadScene("Game");
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
