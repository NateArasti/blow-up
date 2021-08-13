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

    private bool hadSecondTouch;
    private int fingerId = -1;

    private void Update()
    {
        switch (CheckExplosionAction())
        {
            case true:
                onExplosionStartPress.Invoke();
                break;
            case false:
                onExplosionEndPress.Invoke();
                break;
            case null:
                onExplosionPressBreak.Invoke();
                break;
        }
        if (CheckZoomAction()) onZoom.Invoke();
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

    private bool? CheckExplosionAction()
    {
        if (Time.time - lastExplode < CoolDown || isPaused || Input.touchCount == 0) return null;
        if (fingerId == -1)
            fingerId = Input.GetTouch(0).fingerId;
        if (Input.GetTouch(fingerId).phase != TouchPhase.Ended)
        {
            if (!hadSecondTouch) hadSecondTouch = Input.touchCount == 2;
            if (!hadSecondTouch) return true;
        }

        fingerId = -1;
        if (hadSecondTouch)
        {
            hadSecondTouch = false;
            return null;
        }

        if (Time.time - lastExplode > CoolDown)
        {
            lastExplode = Time.time;
            return false;
        }

        return null;
    }

    private bool CheckZoomAction()
    {
        var result = Input.touchCount == 2 && Time.time - lastZoom > CoolDown;
        if (result) lastZoom = Time.time;
        hadSecondTouch = result;
        return result;
    }
}