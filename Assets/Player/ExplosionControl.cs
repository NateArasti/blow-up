using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ExplosionControl : MonoBehaviour
{
#pragma warning disable 649
    [Header("ExplosionData")] 
    [SerializeField]private float radius;
    [SerializeField] private float force;
    [SerializeField] private Stamina stamina;

    [Header("ExplosionEvent")] 
    [SerializeField] private UnityEvent<Vector3> onExplosion;
    [SerializeField] private UnityEvent onWrongExplosion;

    [Header("RandomExplosions")] 
    [SerializeField] private Vector2 randomExplosionTimeDelta;
    [SerializeField] private float chanceOfExplosion;
    private bool randomExplosionsEnabled;

    private Rigidbody2D rigidBody2D;
    private Camera mainCamera;
    private bool isRigidBody2DNull;
    private Attractor blackDotAttractor;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        isRigidBody2DNull = rigidBody2D == null;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(stamina.isRecoveryAmountIncreased && stamina.IsStaminaFull)
        {
            blackDotAttractor.DisableAttraction();
            DisableIncreasedStaminaRecovery();
        }
    }

    public void Explode()
    {
        if (isRigidBody2DNull) return;
        if (!stamina.IsExplosionAvailable)
        {
            onWrongExplosion.Invoke();
            return;
        }
        var mousePosition = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition);
        onExplosion.Invoke(mousePosition);
        var currentPosition = (Vector2) transform.position;
        if ((mousePosition - currentPosition).magnitude > radius) return;
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.AddForce(force * (currentPosition - mousePosition).normalized, ForceMode2D.Impulse);
    }

    public void EnableRandomExplosions()
    {
        if(randomExplosionsEnabled) return;
        randomExplosionsEnabled = true;
        StartCoroutine(RandomExplosions());
    }

    private IEnumerator RandomExplosions()
    {
        if (isRigidBody2DNull) yield break;
        while (true)
        {
            if(Random.value > chanceOfExplosion)
            {
                yield return new WaitForSeconds(Random.Range(randomExplosionTimeDelta.x, randomExplosionTimeDelta.y));
                continue;
            }
            var currentPosition = (Vector2) transform.position;
            var explosionPosition = currentPosition + radius * Random.insideUnitCircle;
            onExplosion.Invoke(explosionPosition);
            rigidBody2D.velocity = Vector2.zero;
            rigidBody2D.AddForce(force * (currentPosition - explosionPosition).normalized, ForceMode2D.Impulse);
            yield return new WaitForSeconds(Random.Range(randomExplosionTimeDelta.x, randomExplosionTimeDelta.y));
        }
    }

    public void UpdateExplosion(float multiplier)
    {
        force *= multiplier;
        radius *= multiplier;
    }

    public void EnableIncreasedStaminaRecovery(Attractor newAttractor)
    {
        stamina.isRecoveryAmountIncreased = true;
        blackDotAttractor = newAttractor;
    }

    public void DisableIncreasedStaminaRecovery()
    {
        stamina.isRecoveryAmountIncreased = false;
        blackDotAttractor = null;
    }
}