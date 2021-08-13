using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatistics : MonoBehaviour
{
    public const string AttemptsKey = "LevelAttempts";
    public const string SumOfScoresKey = "LevelScoreSum";
    public const string HighscoreKey = "Highscore";

    [SerializeField] private Text highscoreText;
    [SerializeField] private Text attemptsText;
    [SerializeField] private Text averageText;

    public void ShowStats()
    {
        var attempts = PlayerPrefs.GetInt($"{LevelSystem.CurrentLevelIndex}_{AttemptsKey}", 0);
        var sumOfScores = PlayerPrefs.GetString($"{LevelSystem.CurrentLevelIndex}_{SumOfScoresKey}", "00:00.00");
        attemptsText.text = $"Attempts:{attempts}";
        if (attempts == 0)
        {
            highscoreText.text = "Highscore:-";
            averageText.text = "Average:-";
            return;
        }

        highscoreText.text = $"Highscore:{PlayerPrefs.GetString($"{LevelSystem.CurrentLevelIndex}_{HighscoreKey}")}";
        var (minutes, seconds, milliseconds) = GetTimerNumbers(sumOfScores);
        var timeSpan = new TimeSpan(new TimeSpan(0, 0, minutes, seconds, milliseconds).Ticks / attempts);
        averageText.text =
            $"Average:{Timer.ConvertTimerNumbersToTimerString(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds)}";
    }

    public static void UpdateStats(string timerText)
    {
        //attempts
        var newAttemptsAmount = PlayerPrefs.GetInt($"{LevelSystem.CurrentLevelIndex}_{AttemptsKey}", 0) + 1;
        PlayerPrefs.SetInt($"{LevelSystem.CurrentLevelIndex}_{AttemptsKey}", newAttemptsAmount);
        //highscore
        if(CheckIfNewHighScore(timerText))
            PlayerPrefs.SetString($"{LevelSystem.CurrentLevelIndex}_{HighscoreKey}", timerText);
        //average
        var (minutes, seconds, milliseconds) = GetTimerNumbers(timerText);
        var sumOfScores = PlayerPrefs.GetString($"{LevelSystem.CurrentLevelIndex}_{SumOfScoresKey}", "00:00.00");
        var (minutes1, seconds1, milliseconds1) = GetTimerNumbers(sumOfScores);
        milliseconds += milliseconds1;
        seconds += milliseconds / 1000 + seconds1;
        milliseconds %= 1000;
        minutes += seconds / 60 + minutes1;
        seconds %= 60;
        var newSumOfScores = Timer.ConvertTimerNumbersToTimerString(minutes, seconds, milliseconds);
        PlayerPrefs.SetString($"{LevelSystem.CurrentLevelIndex}_{SumOfScoresKey}", newSumOfScores);
    }

    public static bool CheckIfNewHighScore(string timerText)
    {
        var (minutes, seconds, milliseconds) = GetTimerNumbers(timerText);
        var highscore = PlayerPrefs.GetString($"{LevelSystem.CurrentLevelIndex}_{HighscoreKey}", "-");
        if (highscore == "-")
            return true;
        var (highMinutes, highSeconds, highMilliseconds) = GetTimerNumbers(highscore);
        if (minutes < highMinutes
            || minutes == highMinutes && seconds < highSeconds
            || minutes == highMinutes && seconds == highSeconds && milliseconds < highMilliseconds)
        {
            return true;
        }

        return false;
    }

    public static (int minutes, int seconds, int milliseconds) GetTimerNumbers(string timerText)
    {
        var numbers = timerText.Split(':', '.');
        return (int.Parse(numbers[0]), int.Parse(numbers[1]), 10 * int.Parse(numbers[2]));
    }
}
