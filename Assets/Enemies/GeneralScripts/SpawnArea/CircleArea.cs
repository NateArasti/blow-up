using UnityEngine;

public class CircleArea : Area
{
    public float Radius => radius;
    [SerializeField] private float radius;

    public override Vector2 GetRandomPositionInArea() => SpawnCenter + Random.insideUnitCircle * Radius;
}