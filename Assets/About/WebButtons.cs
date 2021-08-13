using UnityEngine;
using UnityEngine.UI;

public class WebButtons : MonoBehaviour
{
    [SerializeField] [TextArea] private string url;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(url));
    }
}
