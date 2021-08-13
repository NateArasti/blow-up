using System;
using UnityEngine;
using UnityEngine.UI;

public class EffectPanel : MonoBehaviour
{
    [Header("PickPanel")]
    [SerializeField] private Text description;
    [SerializeField] private Image icon;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowEffect(Sprite iconSprite, string effectDescription)
    {
        description.text = effectDescription;
        icon.sprite = iconSprite;
        animator.SetTrigger("Show");
    }
}
