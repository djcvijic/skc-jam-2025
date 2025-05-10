using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button QuitButton;
    
    [SerializeField] private GameObject SidePanel;
    [SerializeField] private SettingsMenu SettingsMenu;
    [SerializeField] private CreditsMenu CreditsMenu;

    public void Start()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        CreditsButton.onClick.AddListener(OnCreditsButtonClicked);
        QuitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("BabaLevel");
    }
    private void OnSettingsButtonClicked()
    {
        ToggleSettingsPanel();
    }
    private void OnCreditsButtonClicked()
    {
        ToggleCreditsPanel();
    }
    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void ToggleSettingsPanel()
    {
        SidePanel.SetActive(true);
        CreditsMenu.Hide();
        SettingsMenu.Show();
    }
    private void ToggleCreditsPanel()
    {
        SidePanel.SetActive(true);
        SettingsMenu.Hide();
        CreditsMenu.Show();
    }
}