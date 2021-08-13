using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
#pragma warning disable 414
    [SerializeField] private UnityEvent onExplosionStartPress;
    [SerializeField] private UnityEvent onExplosionEndPress;
    [SerializeField] private UnityEvent onZoom;
    [SerializeField] private UnityEvent<bool> onPause;

    private float lastExplode;
    private float lastZoom;
    private const float CoolDown = 0.25f;

    private bool isPaused;

    //private bool hadSecondTouch;
    //private int fingerId = -1;

    private void Update()
    {
        var explosionActionState = CheckExplosionAction();
        if (explosionActionState is true)
            onExplosionStartPress.Invoke();
        else if (explosionActionState is false) 
            onExplosionEndPress.Invoke();
        if (CheckZoomAction()) onZoom.Invoke();
        if (CheckPauseAction()) onPause.Invoke(isPaused);
    }

    public void UnPause()
    {
        lastExplode = Time.time - CoolDown + 0.1f;
        isPaused = false;
        onPause.Invoke(false);
    }

    private bool? CheckExplosionAction()
    {
        if (Time.time - lastExplode < CoolDown || isPaused) return null;
        if (Input.GetMouseButtonDown(0)) return true;
        if (Input.GetMouseButtonUp(0))
        {
            lastExplode = Time.time;
            return false;
        }
        return null;
    }

    private bool CheckZoomAction()
    {
        var result = Time.time - lastZoom > CoolDown && Input.GetMouseButtonUp(1) && !isPaused;
        if (result) lastZoom = Time.time;
        return result;
    }

    private bool CheckPauseAction()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return false;
        isPaused = !isPaused;
        return true;
    }


    //Android Controls
    //private bool ExplosionInput()
    //{
    //    if (Input.touchCount == 0) return false;
    //    if (fingerId == -1)
    //        fingerId = Input.GetTouch(0).fingerId;
    //    if (Input.GetTouch(fingerId).phase != TouchPhase.Ended)
    //    {
    //        if (!hadSecondTouch) hadSecondTouch = Input.touchCount == 2;
    //        return false;
    //    }

    //    fingerId = -1;
    //    if (hadSecondTouch)
    //    {
    //        hadSecondTouch = false;
    //        return false;
    //    }

    //    var result = Time.time - lastExplode > CoolDown;
    //    if (result) lastExplode = Time.time;
    //    return result;

    //}

    //private bool ZoomInput()
    //{
    //    var result = Input.touchCount == 2 && Time.time - lastZoom > CoolDown;
    //    if (result) lastZoom = Time.time;
    //    return result;
    //}
}