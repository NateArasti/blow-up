using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private LevelLoad levelLoader;
    [SerializeField] private LevelStatistics levelStatistics;
    [SerializeField] private int levelIndex;

    private void Start()
    {
        var button = GetComponent<Button>();
        button.interactable = levelIndex <= LevelLoad.FirstNotPassedLevel;
        button.onClick.AddListener(() => levelLoader.SetCurrentLevelIndex(levelIndex));
        button.onClick.AddListener(() => levelStatistics.ShowStats());
    }
}
