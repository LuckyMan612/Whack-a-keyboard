using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject languageButton;
    [SerializeField] private KeyCode starKey;

    private void Update()
    {
        if (Input.GetKeyDown(starKey))
        {
            GameManager.ChangeStateInvoke?.Invoke(GameState.Countdown);
            startText.SetActive(false);
            languageButton.SetActive(false);
            Destroy(this);
        }
    }
}
