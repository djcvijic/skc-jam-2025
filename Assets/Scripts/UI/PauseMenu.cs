using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseRoot;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        ContinueButton.onClick.AddListener(() =>
        {
            PauseRoot.SetActive(false);
            Time.timeScale = 1f;
        });
        QuitButton.onClick.AddListener(() =>
        {
            PauseRoot.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        });
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseRoot.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}