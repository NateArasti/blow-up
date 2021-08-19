using UnityEngine;

public class RectangleArea : Area
{
    public float HalfWidth => 0.5f * width;
    public float HalfHeight => 0.5f * height;
    [SerializeField] private float width, height;

    protected override Vector2 GetRandomPosition() =>
        new Vector2(Random.Range(-HalfWidth, HalfWidth), Random.Range(-HalfHeight, HalfHeight));
}