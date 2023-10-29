using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] float levelLoadDelay = 1f;

    #region GameSession Variables

    public int timeToDeath = 60;
    public int meatScore = 0;
    public bool isGameOver = false;

    #endregion

   Coroutine _coroutineGameTimer;

    private bool isPaused;
    public bool IsPaused => isPaused;
    
    public delegate void PauseGame(bool paused);
    
    public static event PauseGame OnPauseGame;

    public delegate void PlayerDeath();

    public event PlayerDeath OnDeath;
    
    public delegate void GameOver();
    
    public static event GameOver OnGameOver;
    
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
        StartGame();
        SubscribeToDelegatesAndUpdateValues();
    }
    
    void SubscribeToDelegatesAndUpdateValues()
    {
        OnPauseGame += StopGame;
        OnGameOver += RestartGameSession;
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
    
    public void AddScore()
    {
        meatScore++;
    }

    #endregion
    
    #region Manage Game Session

    public void StartGame()
    {
        _coroutineGameTimer = StartCoroutine(HandleGameOver());
    }

    public void RestartGameEvent()
    {
        StartCoroutine(LoadGameReset());
        OnGameOver?.Invoke();
    }
    
    public void RestartGameSession()
    {
        meatScore = 0;
        timeToDeath = 60;
        isGameOver = false;
    }

    public void ExitGameSession()
    {
        RestartGameSession();
        ExitGameSessionEvent();
    }

    public void ExitGameSessionEvent()
    {
        StartCoroutine(LoadExitSession());
    }
    
    public void StopGame(bool paused)
    {
        if (paused)
        {
            StopCoroutine(_coroutineGameTimer);
        } else
        {
            _coroutineGameTimer = StartCoroutine(HandleGameOver());
        }
    }
    
    public void PauseGameEvent(bool paused)
    {
        isPaused = !paused;
        if (isPaused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
        OnPauseGame?.Invoke(isPaused);
    }

    #endregion


    #region Events

    /**
     * When the player dies, the game is over.
     */
    IEnumerator HandleGameOver()
    {
        while (timeToDeath > 0)
        {
         
            yield return new WaitForSeconds(1);
            timeToDeath--;
        }
        Debug.Log("You died. Game Over.");
        isGameOver = true;
        OnDeath?.Invoke();
        SceneTransitionHandler.sceneTransitionHandler.SwitchScene(SceneTransitionHandler.sceneTransitionHandler.DeathSceneName);   
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

    #endregion
    
}