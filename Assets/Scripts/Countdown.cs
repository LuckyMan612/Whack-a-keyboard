using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private int timerMax = 3;
    private void Awake()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;
        countdownText.enabled = false;
    }

    private void GameManager_OnStateChanged(GameState state)
    {
        if (state == GameState.Countdown)
        {
            StartCoroutine(CountdownRoutine());
        }
    }

    private IEnumerator CountdownRoutine()
    {
        float timer = timerMax;
        countdownText.enabled = true;
        while (timer>0)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }
        countdownText.enabled = false;
        GameManager.ChangeStateInvoke?.Invoke(GameState.Game);
    }
}
