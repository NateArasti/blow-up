using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KillableEnemy : MonoBehaviour
{
    public enum KillableEnemyType
    {
        None,
        SimpleDot,
        BigDot
    }

    [SerializeField] private KillableEnemyType type;
    [SerializeField] private int hitsToGetDestroyed;
    [SerializeField] private UnityEvent onDead;

    public int Health => hitsToGetDestroyed;

    private SpriteRenderer spriteRenderer;
    private Color startColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hitsToGetDestroyed--;
            if (hitsToGetDestroyed <= 0)
            {
                if(type == KillableEnemyType.SimpleDot)
                    PlayerPrefs.SetInt(Statistics.SimpleDotKey, PlayerPrefs.GetInt(Statistics.SimpleDotKey, 0) + 1);
                else if(type == KillableEnemyType.BigDot)
                    PlayerPrefs.SetInt(Statistics.BigDotKey, PlayerPrefs.GetInt(Statistics.BigDotKey, 0) + 1);
                onDead.Invoke();
            }
            else
                StartCoroutine(OnHit());
        }
    }

    private IEnumerator OnHit()
    {
        var endColor = Color.white;
        const float smoothness = 10f;
        var startTime = Time.time;
        while (Time.time - startTime < 0.1f)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, endColor, Time.deltaTime * smoothness);
            yield return new WaitForEndOfFrame();
        }
        startTime = Time.time;
        while (Time.time - startTime < 0.1f)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, startColor, Time.deltaTime * smoothness);
            yield return new WaitForEndOfFrame();
        }

        spriteRenderer.color = startColor;
    }
}