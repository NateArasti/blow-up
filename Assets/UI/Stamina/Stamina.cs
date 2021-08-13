using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GainCause
{
    LevelCollision,
    KillableEnemyCollision,
    SharpDot,
}

[Serializable]
public struct GainDictionaryEntry
{
    public GainCause gainCause;
    public float staminaAddMultiplier;
}

public class Stamina : MonoBehaviour
{

    [SerializeField] private float recoveryAmount = 0.5f;
    [SerializeField] private float increasedRecoveryAmountMultiplier = 10;
    [SerializeField] private float explodeCost = 20f;
    [SerializeField] private float gainDelay = 5f;
    [SerializeField] private List<GainDictionaryEntry> gainDictionaryEntry;

    private float IncreasedRecoveryAmount => recoveryAmount * increasedRecoveryAmountMultiplier;

    [HideInInspector] public bool isRecoveryAmountIncreased;

    private Slider slider;
    private float lastGainTime;
    private readonly Dictionary<GainCause, float> gainDictionary = new Dictionary<GainCause, float>();

    public bool IsExplosionAvailable => slider.value > explodeCost;
    public bool IsStaminaFull => Mathf.Abs(slider.value - slider.maxValue) < 1e-5;

    private void Start()
    {
        slider = GetComponent<Slider>();
        foreach (var entry in gainDictionaryEntry)
        {
            gainDictionary.Add(entry.gainCause, entry.staminaAddMultiplier);
        }
    }

    private void Update()
    {
        slider.value += (isRecoveryAmountIncreased ? IncreasedRecoveryAmount : recoveryAmount) * Time.deltaTime;
    }

    public void LoseStamina()
    {
        if (IsExplosionAvailable)
            slider.value -= explodeCost;
    }

    public void GainStamina(GainCause cause)
    {
        var gainValue = gainDictionary[cause] * slider.maxValue;
        if (gainValue < 0)
        {
            slider.value += gainDictionary[cause] * slider.maxValue;
            return;
        }
        if (Time.time - lastGainTime < gainDelay) return;
        lastGainTime = Time.time;
        slider.value += gainDictionary[cause] * slider.maxValue;
    }

    public void ChangeExplodeCost(float multiplier)
    {
        explodeCost *= multiplier;
    }
}