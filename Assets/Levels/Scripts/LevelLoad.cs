using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class LevelLoad : MonoBehaviour, IUnityAdsListener
{
    private const string ADPlacement = "Interstitial_Android";
    private const string AndroidId = "4262437";
    private Action actionAfterAd;

    private static float _lastAdShowTime;
    private const float ADDelta = 50;

    [SerializeField] private int levelCount;

    public static int FirstNotPassedLevel { get; private set; }

    private void Awake()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(AndroidId, false);
            Advertisement.AddListener(this);
        }

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
        if(Time.time - _lastAdShowTime > ADDelta)
        {
            _lastAdShowTime = Time.time;
            ShowAd(() => SceneManager.LoadScene("Game"));
            return;
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadNextLevel()
    {
        LevelSystem.CurrentLevelIndex += 1;
        if (Time.time - _lastAdShowTime > ADDelta)
        {
            _lastAdShowTime = Time.time;
            ShowAd(() => SceneManager.LoadScene("Game"));
            return;
        }
        SceneManager.LoadScene("Game");
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartLevel()
    {
        _lastAdShowTime = Time.time;
        ShowAd(() => SceneManager.LoadScene("Game"));
    }

    private void ShowAd(Action action)
    {
        Advertisement.Load(ADPlacement);
        if (Advertisement.IsReady(ADPlacement))
        {
            actionAfterAd = action;
            Advertisement.Show(ADPlacement);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Time.timeScale = 0;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Time.timeScale = 1;
        actionAfterAd?.Invoke();
    }
}
