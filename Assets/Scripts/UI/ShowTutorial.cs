using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowTutorial : MonoBehaviour
{
    [SerializeField] private GameObject canvasTutorial;
    [SerializeField] private GameObject triggerRoom;

    // Start is called before the first frame update
    void Start()
    {
        canvasTutorial.SetActive(true);
    }

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision)
    {
        canvasTutorial.SetActive(false);
    }
}
