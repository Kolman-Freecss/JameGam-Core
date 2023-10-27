using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject inGameMenu;


    void Start()
    {
        inGameMenu.SetActive(false);
    }

    void Update()
    {
        // Verifica si la tecla de escape se ha presionado
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (!inGameMenu.activeInHierarchy)
        {
            inGameMenu.SetActive(true);
            // TO-DO: Pausar el juego en el GameManager
        }
        else
        {
            inGameMenu.SetActive(false);
        }
    }
}
