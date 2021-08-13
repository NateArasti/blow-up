using UnityEngine;

public class TimeScaleHandler : MonoBehaviour
{
    public bool IsExplosionHold
    {
        get => isExplosionHold;
        set
        {
            if (!slowMotionEnabled) return;
            isExplosionHold = value;
            ChangeTimeScale(isExplosionHold ? slowMotionTimeScale : normalTimeScale);
            timer.slowMotionEnabled = isExplosionHold;
        }
    }
    private bool isExplosionHold;

    public bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            ChangeTimeScale(isPaused ? 0 : normalTimeScale);
        }
    }
    private bool isPaused;

    [SerializeField] private float slowMotionTimeScale;
    [SerializeField] private Timer timer;
    [HideInInspector] public bool slowMotionEnabled;

    private float normalTimeScale = 1;

    private void Start()
    {
        timer.slowMotionMultiplier = slowMotionTimeScale;
        Time.timeScale = 1;
    }

    private void ChangeTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }

    public void SpeedUp(float speedUpTimeScale)
    {
        normalTimeScale = speedUpTimeScale;
    }
}
