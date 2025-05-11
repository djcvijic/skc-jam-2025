using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private TMP_Text WinText;
    [SerializeField] private Button PlayAgainButton;
    
    [SerializeField] private Image Player1WinnerIcon;
    [SerializeField] private Image Player2WinnerIcon;
    [SerializeField] private Image DrawWinnerIcon;
    
    private PlayerScoreBar player1ScoreBar;
    private PlayerScoreBar player2ScoreBar;
    
    private void Start()
    {
        EventsNotifier.Instance.OnGameOverTimerFinished += OnGameTimerEnded;
        PlayAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        FindScoreBars();
    }

    private void FindScoreBars()
    {
        var scoreBars = FindObjectsByType<PlayerScoreBar>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        player1ScoreBar = scoreBars.First((sb) => sb.playerId == 1);
        player2ScoreBar = scoreBars.First((sb) => sb.playerId == 2);
    }

    private void OnGameTimerEnded()
    {
        var player1Score = player1ScoreBar.GetCurrentScore();
        var player2Score = player2ScoreBar.GetCurrentScore();

        var gameOverState = GetGameOverState(player1Score, player2Score);
        WinText.text = GetWinText(gameOverState);
        SetWinnerIcon(gameOverState);
        if (gameOverState == GameOverState.Draw)
            App.Instance.AudioManager.LoseSound();
        else
            App.Instance.AudioManager.WinSound();
        
        Time.timeScale = 0;
        EndScreen.gameObject.SetActive(true);
    }

    private void OnPlayAgainButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private GameOverState GetGameOverState(int player1Score, int player2Score)
    {
        if (player1Score > player2Score)
            return  GameOverState.P1Won;
        if (player2Score > player1Score)
            return  GameOverState.P2Won;
        
        return  GameOverState.Draw;
    }

    private string GetWinText(GameOverState gameOverState)
    {
        switch (gameOverState)
        {
            case GameOverState.P1Won: 
                return "Congratulations. Player 1 Won!";
            case GameOverState.P2Won: 
                return "Congratulations. Player 2 Won!";
            case GameOverState.Draw: 
                return "No Winners. It Was a Draw!";
        }

        return "Time ran out. Play Again?";
    }

    private void SetWinnerIcon(GameOverState gameOverState)
    {
        switch (gameOverState)
        {
            case GameOverState.P1Won: 
                Player1WinnerIcon.gameObject.SetActive(true);
                break;
            case GameOverState.P2Won: 
                Player2WinnerIcon.gameObject.SetActive(true);
                break;
            case GameOverState.Draw: 
                DrawWinnerIcon.gameObject.SetActive(true);
                break;
        }
    }

    public enum GameOverState
    {
        Draw,
        P1Won,
        P2Won
    }
}
