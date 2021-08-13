using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
