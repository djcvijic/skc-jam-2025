using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle MuteSfxToggle;
    [SerializeField] private Toggle MuteMusicToggle;

    private void Start()
    {
        MuteSfxToggle.onValueChanged.AddListener(OnMuteSfxToggled);
        MuteMusicToggle.onValueChanged.AddListener(OnMuteMusicToggled);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnMuteSfxToggled(bool isMuted)
    {
        Debug.Log("SFX Muted: " + isMuted);
        // Example: AudioManager.Instance.SetSfxMuted(isMuted);
    }

    private void OnMuteMusicToggled(bool isMuted)
    {
        Debug.Log("Music Muted: " + isMuted);
        // Example: AudioManager.Instance.SetMusicMuted(isMuted);
    }
}