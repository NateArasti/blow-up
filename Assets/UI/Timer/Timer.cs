using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable IteratorNeverReturns

public class Timer : MonoBehaviour
{
    [SerializeField] private Text finalScore;
    [SerializeField] private GameObject highScoreText;

    [HideInInspector] public bool slowMotionEnabled;
    [HideInInspector] public float slowMotionMultiplier;

    private Text timerText;
    private bool timerStarted;
    private const float NormalDelta = 0.01f;

    private Coroutine timerCountCoroutine;

    private float Delta => slowMotionEnabled ? NormalDelta * slowMotionMultiplier : NormalDelta;

    private void Start()
    {
        timerText = GetComponent<Text>();
    }

    public void StartTimer()
    {
        if(timerStarted) return;
        timerStarted = true;
        timerCountCoroutine = StartCoroutine(TimerCount());
    }

    public void StopTimer()
    {
        timerStarted = false;
        StopCoroutine(timerCountCoroutine);
        finalScore.text = timerText.text;
        highScoreText.SetActive(LevelStatistics.CheckIfNewHighScore(finalScore.text));
        LevelStatistics.UpdateStats(finalScore.text);
    }

    private IEnumerator TimerCount()
    {
        var milliseconds = 0;
        var seconds = 0;
        var minutes = 0;
        while (timerStarted)
        {
            yield return new WaitForSeconds(Delta);
            milliseconds += 10;
            seconds += milliseconds / 1000;
            milliseconds %= 1000;
            minutes += seconds / 60;
            seconds %= 60;
            timerText.text = ConvertTimerNumbersToTimerString(minutes, seconds, milliseconds);
        }
    }

    public static string FormatTimerNumber(int number) => number >= 10 ? $"{number}" : $"0{number}";

    public static string ConvertTimerNumbersToTimerString(int minutes, int seconds, int milliseconds) =>
        $"{FormatTimerNumber(minutes)}:{FormatTimerNumber(seconds)}.{FormatTimerNumber(milliseconds / 10)}";
}
