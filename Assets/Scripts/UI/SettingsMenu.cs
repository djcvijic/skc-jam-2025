using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public bool IsShowing => gameObject.activeInHierarchy;

    [FormerlySerializedAs("MuteSfxToggle")] [SerializeField] private Toggle sfxToggle;
    [FormerlySerializedAs("MuteMusicToggle")] [SerializeField] private Toggle musicToggle;

    private void Start()
    {
        sfxToggle.SetIsOnWithoutNotify(App.Instance.AudioManager.SoundEnabled);
        musicToggle.SetIsOnWithoutNotify(App.Instance.AudioManager.MusicEnabled);
        sfxToggle.onValueChanged.AddListener(OnMuteSfxToggled);
        musicToggle.onValueChanged.AddListener(OnMuteMusicToggled);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnMuteSfxToggled(bool isEnabled)
    {
        App.Instance.AudioManager.SoundEnabled = isEnabled;
        App.Instance.AudioManager.ButtonClick();
    }

    private void OnMuteMusicToggled(bool isEnabled)
    {
        App.Instance.AudioManager.MusicEnabled = isEnabled;
        App.Instance.AudioManager.ButtonClick();
    }
}