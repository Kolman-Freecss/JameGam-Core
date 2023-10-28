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
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .gameSceneName);
    }

    public void Options()
    {
        ButtonClick();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .optionsSceneName);
    }

    public void Credits()
    {
        ButtonClick();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .creditsSceneName);
    }

    public void BackMainMenu()
    {
        ButtonClick();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler
            .defaultMainMenuSceneName);
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