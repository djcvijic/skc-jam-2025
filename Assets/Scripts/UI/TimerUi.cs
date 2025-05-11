using System;
using TMPro;
using UnityEngine;

public class TimerUi : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    
    private float TimePerGame =>  App.Instance.GameSettings.TimePerGame;
    private float timeRemaining;
    private bool timerRunning;

    private void Start()
    {
        timeRemaining = TimePerGame;
        timerRunning = true;
        UpdateTimerText();
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            App.Instance.Notifier.NotifyGameOver();
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}