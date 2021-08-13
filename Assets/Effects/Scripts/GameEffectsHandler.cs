using System.Collections;
using UnityEngine;

public class GameEffectsHandler : MonoBehaviour
{
    private enum EffectState
    {
        None,
        Increased,
        Decreased,
        Handled
    }

    [Header("Player")] 
    [SerializeField] private GameObject player;
    [SerializeField] private float playerExpansionMultiplier;
    [SerializeField] private float playerShrinkMultiplier;
    private EffectState playerScaleEffectState = EffectState.None; 

    [Header("Enemies")] 
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private float enemyExpansionMultiplier;
    [SerializeField] private float enemyShrinkMultiplier;
    private EffectState enemyScaleEffectState = EffectState.None;

    [Header("Stamina")] 
    [SerializeField] private Stamina stamina;
    [SerializeField] private float explosionCostDecreaseMultiplier;
    [SerializeField] private float explosionCostIncreaseMultiplier;
    private EffectState explosionCostEffectState = EffectState.None;

    [Header("Explosion")] 
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosionCircle;
    [SerializeField] private float explosionUpdateMultiplier;
    private EffectState explosionUpdateEffectState = EffectState.None;
    private ExplosionControl explosionControl;

    [Header("Time")] 
    [SerializeField] private float speedUpTimeScale;
    [SerializeField] private TimeScaleHandler timeScaleHandler;
    private EffectState speedUpEffectState = EffectState.None;

    private static GameEffectsHandler _instance;
    private const float Tolerance = 1e-7f;

    private void Awake()
    {
        _instance = this;
        explosionControl = player.GetComponent<ExplosionControl>();
    }

    public static void ApplyEffect(EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.PlayerExpansion:
                _instance.ChangePlayerScale(_instance.playerExpansionMultiplier);
                break;
            case EffectType.PlayerShrink:
                _instance.ChangePlayerScale(_instance.playerShrinkMultiplier);
                break;
            case EffectType.EnemyExpansion:
                _instance.ChangeEnemyScale(_instance.enemyExpansionMultiplier);
                break;
            case EffectType.EnemyShrink:
                _instance.ChangeEnemyScale(_instance.enemyShrinkMultiplier);
                break;
            case EffectType.LessExpensiveExplosions:
                _instance.ChangeExplosionCost(_instance.explosionCostDecreaseMultiplier);
                break;
            case EffectType.MoreExpensiveExplosions:
                _instance.ChangeExplosionCost(_instance.explosionCostIncreaseMultiplier);
                break;
            case EffectType.ExplosionUpdate:
                _instance.UpdateExplosion();
                break;
            case EffectType.SpeedUp:
                _instance.SpeedUp();
                break;
            case EffectType.SlowMotion:
                _instance.timeScaleHandler.slowMotionEnabled = true;
                break;
            case EffectType.RandomExplosions:
                _instance.explosionControl.EnableRandomExplosions();
                break;
        }
    }

    private void SpeedUp()
    {
        if (speedUpEffectState == EffectState.Handled) return;
        speedUpEffectState = EffectState.Handled;
        timeScaleHandler.SpeedUp(speedUpTimeScale);
    }

    private void ChangePlayerScale(float multiplier)
    {
        if (!ChangeIncreaseDecreaseEffectState(ref playerScaleEffectState, playerExpansionMultiplier,
            playerShrinkMultiplier, multiplier))
            return;
        explosion.transform.localScale /= multiplier;
        explosionCircle.transform.localScale /= multiplier;
        player.GetComponent<Rigidbody2D>().mass *= Mathf.Sqrt(multiplier);
        StartCoroutine(ChangeObjectScale(multiplier, player.transform));
    }

    private void UpdateExplosion()
    {
        if(explosionUpdateEffectState == EffectState.Handled) return;
        explosionUpdateEffectState = EffectState.Handled;
        explosionControl.UpdateExplosion(explosionUpdateMultiplier);
        explosion.transform.localScale *= explosionUpdateMultiplier;
        explosionCircle.transform.localScale *= explosionUpdateMultiplier;
    }

    private void ChangeEnemyScale(float multiplier)
    {
        if (!ChangeIncreaseDecreaseEffectState(ref enemyScaleEffectState, enemyExpansionMultiplier,
            enemyShrinkMultiplier, multiplier))
            return;
        enemyController.ChangeEnemyScale(multiplier,
            ((scaleMultiplier, objectTransform) =>
                StartCoroutine(ChangeObjectScale(scaleMultiplier, objectTransform))));
    }

    private void ChangeExplosionCost(float multiplier)
    {
        if (!ChangeIncreaseDecreaseEffectState(ref explosionCostEffectState, explosionCostIncreaseMultiplier,
            explosionCostDecreaseMultiplier, multiplier))
            return;
        stamina.ChangeExplodeCost(multiplier);
    }

    private IEnumerator ChangeObjectScale(float multiplier, Transform objectTransform)
    {
        var startScale = objectTransform.localScale;
        var newScale = multiplier * startScale;
        while (Mathf.Abs(newScale.x - objectTransform.localScale.x) > 1e-3)
        {
            var localScale = objectTransform.localScale;
            localScale = Vector3.Lerp(localScale, newScale, Time.deltaTime);
            objectTransform.localScale = localScale;
            yield return new WaitForEndOfFrame();
            if (objectTransform == null) yield break;
        }

        objectTransform.localScale = newScale;
    }

    private bool ChangeIncreaseDecreaseEffectState(
        ref EffectState effectState,
        float increaseMultiplier,
        float decreaseMultiplier,
        float currentMultiplier)
    {
        switch (effectState)
        {
            case EffectState.None:
                effectState = Mathf.Abs(currentMultiplier - increaseMultiplier) < Tolerance
                    ? EffectState.Increased
                    : EffectState.Decreased;
                break;
            case EffectState.Increased when Mathf.Abs(currentMultiplier - decreaseMultiplier) < Tolerance:
            case EffectState.Decreased when Mathf.Abs(currentMultiplier - increaseMultiplier) < Tolerance:
                effectState = EffectState.None;
                break;
            default:
                return false;
        }

        return true;
    }
}