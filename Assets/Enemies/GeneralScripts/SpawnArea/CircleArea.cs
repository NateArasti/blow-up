using UnityEngine;

public class CircleArea : Area
{
    public float Radius => radius;
    [SerializeField] private float radius;

    protected override Vector2 GetRandomPosition() => Random.insideUnitCircle * Radius;
}