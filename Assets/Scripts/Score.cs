using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int scoreMultiplier;

    public static Action<float> OnScoreAdd;
    public static event Action<float> SendEndScore; 

    private float _score;
    private void Awake()
    {
        OnScoreAdd += AddScore;
        GameManager.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(GameState state)
    {
        if (state is GameState.GameLose or GameState.GameWon)
        {
            SendEndScore?.Invoke(_score);
        }
    }

    private void AddScore(float value)
    {
        _score += Mathf.CeilToInt(value*scoreMultiplier);
        scoreText.text = $"{_score} pts";
    }

    private void OnDisable()
    {
        OnScoreAdd = null;
        SendEndScore = null;
    }
}
