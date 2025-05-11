using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{
    public bool IsShowing => gameObject.activeInHierarchy;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}