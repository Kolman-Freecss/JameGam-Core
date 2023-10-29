using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject canvasNote;
    
    #region InitData

    void Start()
    {
        inGameMenu.SetActive(false);
        PlayerBehaviour.Instance.Inputs.OnEscapeTrigger += OnPressEscape;
    }

    #endregion

    public void CloseNote()
    {
        canvasNote.SetActive(false);
    }
    
    public void OpenNote()
    {
        canvasNote.SetActive(true);
    }
    
    void OnPressEscape(bool pressed)
    {
        if (PlayerBehaviour.Instance.Inputs.escape)
        {
            PauseGame(GameManager.Instance.IsPaused);
        }
    }

    public void PauseGame(bool paused)
    {
        GameManager.Instance.PauseGameEvent(paused);
        inGameMenu.SetActive(GameManager.Instance.IsPaused);
    }
    
    public void OnExitMenu()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        GameManager.Instance.ExitGameSession();
    }
    
}