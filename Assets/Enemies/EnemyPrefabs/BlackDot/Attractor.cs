using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] private float gravityForce;
    private bool attractionEnabled;
    private Rigidbody2D player;
    private ExplosionControl playerExplosionControl;

    private bool playerTouched;

    public void EnableAttraction(Rigidbody2D newRigidbodyToAttract)
    {
        player = newRigidbodyToAttract;
        playerExplosionControl = player.GetComponent<ExplosionControl>();
        attractionEnabled = true;
    }

    public void DisableAttraction()
    {
        player = null;
        attractionEnabled = false;
    }

    private void FixedUpdate()
    {
        if(!attractionEnabled) return;
        var direction = (Vector2) transform.position - player.position;
        var force = gravityForce / direction.magnitude;
        player.AddForce(direction.normalized * force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!playerTouched)
            {
                PlayerPrefs.SetInt(Statistics.BlackDotKey, PlayerPrefs.GetInt(Statistics.BlackDotKey, 0) + 1);
                playerTouched = true;
            }
            playerExplosionControl.EnableIncreasedStaminaRecovery(this);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerExplosionControl.DisableIncreasedStaminaRecovery();
        }
    }
}
