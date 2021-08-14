using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
#pragma warning disable 414
    [SerializeField] private UnityEvent onExplosionStartPress;
    [SerializeField] private UnityEvent onExplosionPressBreak;
    [SerializeField] private UnityEvent onExplosionEndPress;
    [SerializeField] private UnityEvent onZoom;
    [SerializeField] private UnityEvent<bool> onPause;

    private float lastExplode;
    private float lastZoom;
    private const float CoolDown = 0.25f;

    private bool isPaused;

    private void Update()
    {
        if(isPaused) return;
        if(Input.GetMouseButtonDown(0)) onExplosionStartPress.Invoke();
        else if (Input.GetMouseButtonUp(0) && Time.time - lastExplode > CoolDown && Time.time - lastZoom > CoolDown)
        {
            onExplosionEndPress.Invoke();
            lastExplode = Time.time;
        }
        else if (Input.GetMouseButtonDown(1) && Time.time - lastZoom > CoolDown)
        {
            onExplosionPressBreak.Invoke();
            onZoom.Invoke();
            lastZoom = Time.time;
        }
    }

    public void UnPause()
    {
        lastExplode = Time.time - CoolDown + 0.1f;
        isPaused = false;
        onPause.Invoke(false);
    }

    public void Pause()
    {
        isPaused = true;
        onPause.Invoke(true);
    }
}