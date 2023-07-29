using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoleSpawner : MonoBehaviour
{
    Key[] keys;
    private AudioSource source;
    void Awake()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;
        keys = FindObjectsOfType<Key>();
        source = GetComponent<AudioSource>();
    }

    [SerializeField] float maxDelay = 2, minDelay = .5f, difficultyIncreaseSpeed = 1;

    float Delay(int i) => (maxDelay - minDelay) * Mathf.Exp(-difficultyIncreaseSpeed * i) + minDelay;

    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip countDown;
    void Spawn() => StartCoroutine(SpawnRoutine());
    
    private void GameManager_OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.WaitingForStart:
                break;
            case GameState.Countdown:
                source.clip = countDown;
                source.Play();
                break;
            case GameState.Game:
                Spawn();
                break;
            case GameState.GameLose:
                Lose();
                break;
            case GameState.GameWon:
                this.enabled = false;
                Win();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < 10000; i++)
        {
            yield return new WaitForSeconds(Delay(i));
            Mole();
        }
    }

    void Mole()
    {
        Key key = keys[Random.Range(0, keys.Length)];
        key.Mole();
    }

    void Lose()
    {
        source.clip = loseClip;
        source.Play();

    }

    void Win()
    {
        source.clip = winClip;
        source.Play();
    }
}