using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] float levelLoadDelay = 1f;

    public int timeToDeath = 60;
    public int meatScore = 0;
    public bool isGameOver = false;

   // [SerializeField] TextMeshProUGUI timeText;
   // [SerializeField] TextMeshProUGUI meatText;
   Coroutine _coroutineGameTimer;

    private bool isPaused;
    public bool IsPaused => isPaused;
    
    public delegate void PauseGame(bool paused);
    
    public static event PauseGame OnPauseGame;

    public delegate void PlayerDeath();

    public event PlayerDeath OnGameOver;
    
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
        //timeText.text = timeToDeath.ToString();
       // meatText.text = meatScore.ToString();
    }
    
    void SubscribeToDelegatesAndUpdateValues()
    {
        OnPauseGame += StopGame;
    }

    #endregion

    public void Update()
    {
        if (isGameOver)
        {
            return;
        }
       // timeText.text = timeToDeath.ToString();
        //meatText.text = meatScore.ToString();
    }

    public void StartGame()
    {
        _coroutineGameTimer = StartCoroutine(HandleGameOver());
    }

    public void AddScore()
    {
        meatScore++;
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
    
    void ResetGameSession()
    {
        StartCoroutine(LoadGameReset());
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

    #region Events

    IEnumerator HandleGameOver()
    {
        while (timeToDeath > 0)
        {
         
            yield return new WaitForSeconds(1);
            timeToDeath--;
        }
        Debug.Log("Game Over");
        isGameOver = true;
        OnGameOver?.Invoke();
        ResetGameSession();
    }

    IEnumerator LoadGameReset()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    #endregion
    
}