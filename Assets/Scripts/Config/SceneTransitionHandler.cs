using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneTransitionHandler : MonoBehaviour
{
    public static SceneTransitionHandler sceneTransitionHandler { get; private set; }

    [FormerlySerializedAs("DefaultMainMenuSceneName")] [SerializeField]
    public String defaultMainMenuSceneName = "Intro";
    public String gameSceneName = "InGame";
    public String gameOverSceneName = "GameOver";
    public String optionsSceneName = "Options";
    public String creditsSceneName = "Credits";
    
    public enum SceneStates
    {
        Intro,
        InGame,
        Options,
        GameOver,
        Credits
    }

    private SceneStates m_SceneState;

    #region InitData

    private void Awake()
    {
        if (sceneTransitionHandler != null)
        {
            Destroy(gameObject);
        }
        else
        {
            sceneTransitionHandler = this;
            SetSceneState(SceneStates.Intro);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        if (m_SceneState == SceneStates.Intro)

        {
            SceneManager.LoadScene(defaultMainMenuSceneName);
        }
    }

    #endregion

    public SceneStates GetSceneStateByName(string sceneName)
    {
        switch (sceneName)
        {
            case "Intro":
                return SceneStates.Intro;
            case "InGame":
                return SceneStates.InGame;
            case "Options":
                return SceneStates.Options;
            case "GameOver":
                return SceneStates.GameOver;
            case "Credits":
                return SceneStates.Credits;
            default:
                throw new ArgumentException("Nombre de escena no válido");
        }
    }
    
    public void ExitAndLoadStartMenu()
    {
        SwitchScene(defaultMainMenuSceneName);
    }

    public void SwitchScene(string scenename)
    {
        SetSceneState(GetSceneStateByName(scenename));
        SceneManager.LoadScene(scenename);
    }

    #region Getter & Setter

    public SceneStates GetCurrentSceneState()
    {
        return m_SceneState;
    }

    private void SetSceneState(SceneStates state)
    {
        m_SceneState = state;
    }

    #endregion
}