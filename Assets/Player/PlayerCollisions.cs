using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct CollisionSoundEntry
{
    public GainCause gainCause;
    public AudioClip audioClip;
}

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private UnityEvent<GainCause> onCollision1;
    [SerializeField] private UnityEvent<AudioClip> onCollision2;
    [SerializeField] private UnityEvent<GameObject> onEnemyKill;
    [SerializeField] private UnityEvent<Vector3> onWallHit;
    [SerializeField] private List<CollisionSoundEntry> collisionSound;

    private readonly Dictionary<GainCause, AudioClip> collisionSoundDictionary = new Dictionary<GainCause, AudioClip>();

    private void Start()
    {
        foreach (var data in collisionSound) 
            collisionSoundDictionary.Add(data.gainCause, data.audioClip);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LevelEnvironment"))
        {
            onWallHit.Invoke(other.GetContact(0).point);
            onCollision1.Invoke(GainCause.LevelCollision);
            onCollision2.Invoke(collisionSoundDictionary[GainCause.LevelCollision]);
        }
        else if (other.gameObject.CompareTag("KillableEnemy"))
        {
            onCollision1.Invoke(GainCause.KillableEnemyCollision);
            onCollision2.Invoke(collisionSoundDictionary[GainCause.KillableEnemyCollision]);
            onEnemyKill.Invoke(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SharpDot"))
        {
            other.gameObject.GetComponent<ParticleSystem>().Play();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            onCollision1.Invoke(GainCause.SharpDot);
            onCollision2.Invoke(collisionSoundDictionary[GainCause.SharpDot]);
        }
    }
}