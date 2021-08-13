using UnityEngine;
using UnityEngine.Events;

public class AttractionArea : MonoBehaviour
{
    [SerializeField] private UnityEvent<Rigidbody2D> onAreaEnter;
    [SerializeField] private UnityEvent onAreaExit;
    [SerializeField] private PhysicsMaterial2D bounceMaterial;
    private GameObject enteredGameObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onAreaEnter.Invoke(other.GetComponent<Rigidbody2D>());
            enteredGameObject = other.gameObject;
            enteredGameObject.GetComponent<Rigidbody2D>().sharedMaterial = null;
            enteredGameObject.GetComponent<CircleCollider2D>().sharedMaterial = null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onAreaExit.Invoke();
        enteredGameObject.GetComponent<Rigidbody2D>().sharedMaterial = bounceMaterial;
        enteredGameObject.GetComponent<CircleCollider2D>().sharedMaterial = bounceMaterial;
    }
}
