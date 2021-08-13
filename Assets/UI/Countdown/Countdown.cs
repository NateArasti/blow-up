using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private static readonly int Count = Animator.StringToHash("Count");

    [SerializeField] private int totalCount = 3;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UnityEvent onCountdownEnd;
    [SerializeField] private AudioClip countdownStep;
    [SerializeField] private AudioClip countdownEnd;

    private Text countDownText;
    private Animator animator;
    

    private void Start()
    {
        countDownText = GetComponent<Text>();
        countDownText.text = $"{totalCount}";
        animator = GetComponent<Animator>();
        StartCoroutine(CountdownStart());
    }

    private IEnumerator CountdownStart()
    {
        for (; totalCount > 0; totalCount--)
        {
            countDownText.text = $"{totalCount}";
            animator.SetTrigger(Count);
            audioSource.PlayOneShot(countdownStep);
            yield return new WaitForSeconds(1.1f);
        }
        
        countDownText.text = "GO";
        animator.SetTrigger(Count);
        audioSource.PlayOneShot(countdownEnd);
        yield return new WaitForSeconds(1f);

        onCountdownEnd.Invoke();
    }
}
