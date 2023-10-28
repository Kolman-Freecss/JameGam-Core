using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

 

    public void NewGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Credits ()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene("Intro");
    }
}
