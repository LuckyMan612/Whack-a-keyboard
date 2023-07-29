using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endUiObject;
    [SerializeField] private TextMeshProUGUI endStatusText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        endUiObject.SetActive(false);
        GameManager.OnStateChanged += GameManager_OnStateChanged;
        Score.SendEndScore += f =>
        {
            endScoreText.text = $"Score: {f} pts";
        };
    }

    private void GameManager_OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.WaitingForStart:
                break;
            case GameState.Countdown:
                break;
            case GameState.Game:
                break;
            case GameState.GameLose:
                Show("Game Over!");
                break;
            case GameState.GameWon:
                Show("Game Won!");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void Show(string endStatusString)
    {
        endUiObject.SetActive(true);
        endStatusText.text = endStatusString;
    }
}
