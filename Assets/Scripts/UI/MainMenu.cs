using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button InstructionsButton;
    [SerializeField] private Button QuitButton;
    
    
    [SerializeField] private GameObject SidePanel;
    [SerializeField] private SettingsMenu SettingsMenu;
    [SerializeField] private GameObject InstructionsMenu;
    [SerializeField] private CreditsMenu CreditsMenu;
    
    public void Start()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        CreditsButton.onClick.AddListener(OnCreditsButtonClicked);
        InstructionsButton.onClick.AddListener(OnInstructionsButtonClicked);
        QuitButton.onClick.AddListener(OnQuitButtonClicked);
        App.Instance.AudioManager.StartMainMenuMusic();
    }

    private void OnInstructionsButtonClicked()
    {
        App.Instance.AudioManager.ButtonClick();
        ToggleInstructionsPanel();
    }

    private void OnPlayButtonClicked()
    {
        App.Instance.AudioManager.ButtonClick();
        App.Instance.AudioManager.StartLevelMusic();
        SceneManager.LoadScene("BabaLevel");
    }
    private void OnSettingsButtonClicked()
    {
        App.Instance.AudioManager.ButtonClick();
        ToggleSettingsPanel();
    }
    private void OnCreditsButtonClicked()
    {
        App.Instance.AudioManager.ButtonClick();
        ToggleCreditsPanel();
    }
    private void OnQuitButtonClicked()
    {
        App.Instance.AudioManager.ButtonClick();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ToggleSettingsPanel()
    {
        SidePanel.SetActive(!SettingsMenu.IsShowing);
        CreditsMenu.Hide();
        InstructionsMenu.SetActive(false);
        SettingsMenu.Show();
    }
    private void ToggleCreditsPanel()
    {
        SidePanel.SetActive(!CreditsMenu.IsShowing);
        SettingsMenu.Hide();
        InstructionsMenu.SetActive(false);
        CreditsMenu.Show();
    }
    
    private void ToggleInstructionsPanel()
    {
        SidePanel.SetActive(!InstructionsMenu.activeInHierarchy);
        InstructionsMenu.SetActive(true);
        SettingsMenu.Hide();
        CreditsMenu.Hide();
    }
}