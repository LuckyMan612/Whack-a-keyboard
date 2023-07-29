using System;
using UnityEngine;

public enum GameState
{
    WaitingForStart,
    Countdown,
    Game,
    GameLose,
    GameWon
}

public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnStateChanged;
    public static Action<GameState> ChangeStateInvoke;

    
    private GameState _currentState;

    private void OnEnable()
    {
        ChangeStateInvoke += ChangeState;
    }

    private void ChangeState(GameState newState)
    {
        if (newState == _currentState)
        {
            print("This state is already running");
            return;
        }
        _currentState = newState;
        print($"State: {_currentState.ToString()}");
        OnStateChanged?.Invoke(_currentState);
    }

    private void OnDisable()
    {
        ChangeStateInvoke = null;
        OnStateChanged = null;
    }
}
