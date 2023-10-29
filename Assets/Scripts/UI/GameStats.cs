using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt_meat;
    [SerializeField] TextMeshProUGUI txt_timer;

    void Start()
    {
        txt_timer.text = GameManager.Instance.timeToDeath.ToString();
        txt_meat.text = GameManager.Instance.meatScore.ToString();
    }

    void Update()
    {
        txt_timer.text = GameManager.Instance.timeToDeath.ToString();
        txt_meat.text = GameManager.Instance.meatScore.ToString();
    }
}
