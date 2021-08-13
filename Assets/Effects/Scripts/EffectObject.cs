using UnityEngine;
using UnityEngine.Events;

public class EffectObject : MonoBehaviour
{
    [SerializeField] private Effect[] effects;
    [SerializeField] private UnityEvent onPick;

    private Effect effect;
    private EffectPanel effectPanel;

    private void Awake()
    {
        effect = effects[Random.Range(0, effects.Length)];
        effectPanel = GameObject.FindGameObjectWithTag("EffectPanel").GetComponent<EffectPanel>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEffectsHandler.ApplyEffect(effect.Type);
            effectPanel.ShowEffect(effect.Sprite, effect.Description);
            onPick.Invoke();
        }
    }
}
