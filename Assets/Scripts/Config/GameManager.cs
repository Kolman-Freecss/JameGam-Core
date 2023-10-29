using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] float gameWinDelay = 1f;

    #region GameSession Variables

    public int timeToDeath = 60;
    public int meatScore = 0;
    public bool isGameOver = false;

    #endregion

   Coroutine _coroutineGameTimer;

    private bool isPaused;
    public bool IsPaused => isPaused;

    #region Event Variables

    public delegate void PauseGame(bool paused);
    
    public static event PauseGame OnPauseGame;

    public delegate void PlayerDeath();

    public event PlayerDeath OnDeath;
    
    public delegate void GameOver();
    
    public static event GameOver OnGameOver;

    public delegate void WinGame();
    
    public static event WinGame OnWinGame;

    #endregion
    
    #region InitData

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        RestartGameSession();
        SubscribeToDelegatesAndUpdateValues();
    }
    
    void SubscribeToDelegatesAndUpdateValues()
    {
        OnPauseGame += StopGame;
        OnGameOver += RestartGameSession;
    }
    
    void UnsubscribeToDelegates()
    {
        OnPauseGame -= StopGame;
        OnGameOver -= RestartGameSession;
    }

    #endregion

    #region Game Logic

    public void Update()
    {
        if (isGameOver)
        {
            return;
        }
    }
    
    /**
     * Invoke this method when the player eat on LeashGrab.cs
     */
    public void AddScore(int score)
    {
        meatScore += score;
    }

    #endregion
    
    #region Manage Game Session

    /**
     * Invoke this method when you start a new game.
     */
    public void StartGame()
    {
        RestartGameSession();
        //TODO: Put this routine when you exit from the first zone.
        _coroutineGameTimer = Instance.StartCoroutine(HandleGameOver());
    }

    public void RestartGameEvent()
    {
        Instance.StartCoroutine(LoadGameReset());
        OnGameOver?.Invoke();
        _coroutineGameTimer = Instance.StartCoroutine(HandleGameOver());
    }
    
    public void RestartGameSession()
    {
        Instance.meatScore = 0;
        Instance.timeToDeath = 60;
        Instance.isGameOver = false;
    }

    public void ExitGameSession()
    {
        RestartGameSession();
        ExitGameSessionEvent();
    }

    public void ExitGameSessionEvent()
    {
        Instance.StartCoroutine(LoadExitSession());
    }
    
    public void StopGame(bool paused)
    {
        if (paused)
        {
            StopCoroutine(_coroutineGameTimer);
        } else
        {
            _coroutineGameTimer = Instance.StartCoroutine(HandleGameOver());
        }
    }
    
    public void PauseGameEvent(bool paused)
    {
        Instance.isPaused = !paused;
        if (isPaused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
        OnPauseGame?.Invoke(isPaused);
    }

    public void WinGameEvent()
    {
        Instance.isGameOver = true;
        OnWinGame?.Invoke();
        Instance.StartCoroutine(HandleWinEvent());
    }

    #endregion

    #region Events

    public void OnNewGame()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        RestartGameEvent();
    }
    
    public void OnExitMenu()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        ExitGameSession();
    }
    
    public void OnCredits()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler.CreditsSceneName);
    }
    
    /**
     * When the player dies, the game is over.
     */
    IEnumerator HandleGameOver()
    {
        while (Instance.timeToDeath > 0)
        {
            yield return new WaitForSeconds(1);
            Instance.timeToDeath--;
        }
        Debug.Log("You died. Game Over.");
        isGameOver = true;
        OnDeath?.Invoke();
        yield return new WaitForSeconds(2);
        
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler.DeathSceneName);   
    }

    IEnumerator HandleWinEvent()
    {
        Debug.Log("You win. Game Over.");
        yield return new WaitForSeconds(gameWinDelay);
        
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler.WinSceneName);
    }

    IEnumerator LoadGameReset()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler.GameSceneName);
    }
    
    IEnumerator LoadExitSession()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler.DefaultMainMenuSceneName);
    }

    private void OnDestroy()
    {
        Debug.Log("GameManager destroyed");
        UnsubscribeToDelegates();
    }

    private void OnDisable()
    {
        Debug.Log("GameManager disabled");
        UnsubscribeToDelegates();
    }

    #endregion
    
}