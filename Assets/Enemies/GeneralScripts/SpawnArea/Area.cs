using UnityEngine;

public abstract class Area : MonoBehaviour
{
    public Vector2 SpawnCenter => offset + (Vector2) transform.position;
    [SerializeField] private Vector2 offset;

    private float? angle;

    private void Start()
    {
        angle = transform.eulerAngles.z * Mathf.PI / 180;
    }

    protected abstract Vector2 GetRandomPosition();

    public Vector2 GetRandomPositionInArea() => SpawnCenter + RotatePositionAroundCenter(GetRandomPosition());

    public Vector2 RotatePositionAroundCenter(Vector2 position)
    {
        var newAngle = this.angle ?? transform.eulerAngles.z * Mathf.PI / 180;
        var cos = Mathf.Cos(newAngle);
        var sin = Mathf.Sin(newAngle);
        return new Vector2(position.x * cos - position.y * sin,
            position.x * sin + position.y * cos);
    }
}