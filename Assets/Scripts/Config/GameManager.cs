using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int timeToDeath = 60;
    public int meatScore = 0;

    public bool isGameOver = false;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI meatText;

    public delegate void PlayerDeath();

    public event PlayerDeath OnGameOver;

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
        timeText.text = timeToDeath.ToString();
        meatText.text = meatScore.ToString();
    }

    public void Update()
    {
        if (isGameOver)
        {
            return;
        }
        timeText.text = timeToDeath.ToString();
        meatText.text = meatScore.ToString();
    }

    public void StartGame()
    {
        StartCoroutine(HandleGameOver());
    }

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
    }
}