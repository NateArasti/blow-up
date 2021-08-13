using UnityEngine;

public class SharpDotStatistics : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(Statistics.SharpDotKey, PlayerPrefs.GetInt(Statistics.SharpDotKey, 0) + 1);
        }
    }
}
