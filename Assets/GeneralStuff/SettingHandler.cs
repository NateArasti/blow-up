using UnityEngine;
using UnityEngine.Events;

public class SettingHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> onBoolStateChange;
    [SerializeField] private UnityEvent<float> onFloatStateChange;
    [SerializeField] private string settingName;
    private bool currentBoolState;
    private float currentFloatState;//[0,1]

    private void Start()
    {
        currentFloatState = PlayerPrefs.GetFloat(settingName, 0.1f);
        currentBoolState = currentFloatState != 0;
        onFloatStateChange.Invoke(currentFloatState);
        onBoolStateChange.Invoke(currentBoolState);
    }

    public void ChangeFloatState(float floatState)
    {
        currentFloatState = floatState;
        onFloatStateChange.Invoke(currentFloatState);
        if (currentBoolState != (currentFloatState != 0))
        {
            currentBoolState = currentFloatState != 0;
            onBoolStateChange.Invoke(currentBoolState);
        }
        PlayerPrefs.SetFloat(settingName, currentFloatState);
    }

    public void ChangeBoolState()
    {
        currentBoolState = !currentBoolState;
        if (currentBoolState)
        {
            onFloatStateChange.Invoke(0.1f);
            onBoolStateChange.Invoke(currentBoolState);
            PlayerPrefs.SetFloat(settingName, currentFloatState);
        }
        else
        {
            onFloatStateChange.Invoke(0);
            onBoolStateChange.Invoke(currentBoolState);
            PlayerPrefs.SetFloat(settingName, 0);
        }
    }
}
