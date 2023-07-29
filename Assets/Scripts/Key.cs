using System;
using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] string key;
    KeyCode keyCode;
    bool Pressed => Input.GetKeyDown(keyCode);
    [SerializeField] bool autoTextUI = true;
    
    [SerializeField] private float scoreMax = 5;
    
    Animator anim;
    private AudioSource source;
    [SerializeField]private AudioClip keyClip;
    [SerializeField]private AudioClip wrongClip;
    

    float midT = .25f;

    private float _scoreToAdd;

    private bool _isMole;

    void Awake()
    {
        GameManager.OnStateChanged += GameManager_OnStateChanged;

        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key, true);
        T = midT;
        if (autoTextUI) GetComponentInChildren<TMPro.TextMeshProUGUI>().text = key;

        this.enabled = false;
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
                this.enabled = true;
                break;
            case GameState.GameLose:
            case GameState.GameWon:
                this.enabled = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    float t = 0;
    float T
    {
        get { return t; }
        set
        {
            t = Mathf.Clamp01(value);
            anim.SetFloat("t", t);
            if (value <= 0) Ont0();
        }
    }

    [SerializeField] float speed = 1;

    float dir = 0;

    void Update()
    {
        if (_scoreToAdd > 0)
        {
            _scoreToAdd -= Time.deltaTime;
        }
        else
        {
            if (_isMole)
            {
                source.clip = wrongClip;
                source.Play();
                FindObjectOfType<HpManager>().LoseHP();
                _isMole = false;
                GetComponent<Animator>().SetTrigger("down");
                dir = -5;
            }
                
        }

        if(GameObject.Find("RightAlt").GetComponent<Key>().Pressed == true && key == "LeftControl") //This to stop LeftCtrl also clicking when AltGr (RightAlt) clicked
        {

        }
        else if (Pressed)
        {
            dir = -5;
            if (_scoreToAdd > 0 && _isMole)
            {
                source.clip = keyClip;
                source.Play();

                Score.OnScoreAdd?.Invoke(_scoreToAdd);
                _scoreToAdd = 0;
                _isMole = false;
                
            }
            else
            {
                Score.OnScoreAdd?.Invoke(-3);
                source.clip = keyClip;
                source.Play();
            }
        }

        T += speed * dir * Time.deltaTime;
    }

   

    void Ont0() => StartCoroutine(Ont0Routine());

    IEnumerator Ont0Routine()
    {
        dir = 0;
        yield return new WaitForSeconds(.5f);
        dir = 1;
        yield return new WaitUntil(() => T >= midT);
        dir = 0;
        T = midT;
        yield return new WaitForSeconds(1f);
    }

    public void Mole()
    {
        dir = 1;
        _isMole = true;
        _scoreToAdd = scoreMax;
    }
}
