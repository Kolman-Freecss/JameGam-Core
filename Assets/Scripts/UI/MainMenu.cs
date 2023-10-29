using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    #region InitData

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public void NewGame()
    {
        ButtonClick();
        GameManager.Instance.StartGame();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .GameSceneName);
    }

    public void Options()
    {
        ButtonClick();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .OptionsSceneName);
    }

    public void Credits()
    {
        ButtonClick();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .CreditsSceneName);
    }

    public void BackMainMenu()
    {
        ButtonClick();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .DefaultMainMenuSceneName);
    }

    public void ExitGame()
    {
        ButtonClick();
        Application.Quit();
    }
    
    public void ButtonClick()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
    }
    
}