using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt_meat;
    [SerializeField] TextMeshProUGUI txt_timer;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        txt_timer.text = gameManager.timeToDeath.ToString();
        txt_meat.text = gameManager.meatScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txt_timer.text = gameManager.timeToDeath.ToString();
        txt_meat.text = gameManager.meatScore.ToString();
    }
}
