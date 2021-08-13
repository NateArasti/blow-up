using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
public class Effect : ScriptableObject
{
    [SerializeField] private EffectType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] [TextArea] private string description;

    public EffectType Type => type;
    public Sprite Sprite => sprite;
    public string Description => description;
}
