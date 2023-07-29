using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private float timerMax = 30;

    private IEnumerator _coroutine;

    private void Awake()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;
        timerImage.fillAmount = 1;
        _coroutine = TimerCoroutine();
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
                StartCoroutine(_coroutine);
                break;
            case GameState.GameLose:
            case GameState.GameWon:
                StopCoroutine(_coroutine);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private IEnumerator TimerCoroutine()
    {
        float timer = timerMax;
        
        while (timer>0)
        {
            timerImage.fillAmount = timer / timerMax;
            
            timer -= Time.deltaTime;
            
            yield return null;
        }
        GameManager.ChangeStateInvoke?.Invoke(GameState.GameWon);
    }
}
