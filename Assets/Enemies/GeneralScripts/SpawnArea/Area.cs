using UnityEngine;

public abstract class Area : MonoBehaviour
{
    public Vector2 SpawnCenter => offset + (Vector2) transform.position;
    [SerializeField] private Vector2 offset;

    public abstract Vector2 GetRandomPositionInArea();
}