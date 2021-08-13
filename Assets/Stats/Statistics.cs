using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    public const string SimpleDotKey = "KilledSimpleDots";
    public const string BigDotKey = "KilledBigDots";
    public const string SharpDotKey = "TouchedSharpDots";
    public const string BlackDotKey = "TouchedBlackDots";

    [SerializeField] private Text simpleDotStats;
    [SerializeField] private Text bigDotStats;
    [SerializeField] private Text sharpDotStats;
    [SerializeField] private Text blackDotStats;

    private void Awake()
    {
        simpleDotStats.text = $"{PlayerPrefs.GetInt(SimpleDotKey, 0)}";
        bigDotStats.text = $"{PlayerPrefs.GetInt(BigDotKey, 0)}";
        sharpDotStats.text = $"{PlayerPrefs.GetInt(SharpDotKey, 0)}";
        blackDotStats.text = $"{PlayerPrefs.GetInt(BlackDotKey, 0)}";
    }
}
