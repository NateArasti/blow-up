using UnityEngine;

public class SecondBoss : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSlowDown;
    private Transform player;
    private Transform thisTransform;

    private void Start()
    {
        thisTransform = transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        //Rotate
        var playerPosition = player.position;
        var thisPosition = thisTransform.position;
        thisTransform.Rotate(0, 0,
            Vector2.SignedAngle(thisTransform.right, playerPosition - thisPosition) * rotationSpeed * Time.fixedDeltaTime);
        //Move
        thisTransform.position =
            Vector3.Lerp(thisPosition, playerPosition, movementSlowDown * Time.fixedDeltaTime);
    }

    public void MoveFaster(float multiplier)
    {
        rotationSpeed *= multiplier;
        movementSlowDown *= multiplier;
    }
}
