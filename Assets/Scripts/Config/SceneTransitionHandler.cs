using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneTransitionHandler : MonoBehaviour
{
    public static SceneTransitionHandler sceneTransitionHandler { get; private set; }

    [FormerlySerializedAs("DefaultMainMenuSceneName")] [SerializeField]
    public string DefaultMainMenuSceneName = "Intro";
    public string GameSceneName = "InGame";
    public string GameOverSceneName = "GameOver";
    public string DeathSceneName = "Death";
    public string OptionsSceneName = "Options";
    public string CreditsSceneName = "Credits";
    public string WinSceneName = "Win";
    
    public enum SceneStates
    {
        Intro,
        InGame,
        Options,
        GameOver,
        Credits,
        Death,
        Win
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
            SceneManager.LoadScene(DefaultMainMenuSceneName);
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
            case "Death":
                return SceneStates.Death;
            case "Win":
                return SceneStates.Win;
            default:
                throw new ArgumentException("No scene name found");
        }
    }
    
    public void ExitAndLoadStartMenu()
    {
        SwitchScene(DefaultMainMenuSceneName);
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