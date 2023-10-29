using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 1.0f;
    public float targetZoom = 5.0f;
    private float initialSize;

    private Camera mainCamera;
    SceneTransitionHandler.SceneStates actualScene;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        actualScene = SceneTransitionHandler.sceneTransitionHandler.GetCurrentSceneState();
        initialSize = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        SceneTransitionHandler.SceneStates newScene = SceneTransitionHandler.sceneTransitionHandler.GetCurrentSceneState();
        if (newScene != actualScene)
        {
            //Cambio de zoom dependiendo de la escena
            switch (actualScene)
            {
                case SceneTransitionHandler.SceneStates.Intro:
                    break;
                case SceneTransitionHandler.SceneStates.InGame:
                    float aspectRatio = (float)Screen.width / (float)Screen.height;
                    Debug.Log("Entra");
                    mainCamera.orthographicSize = 100f;
                    break;
                case SceneTransitionHandler.SceneStates.Options:
                    break;
                case SceneTransitionHandler.SceneStates.GameOver:
                    break;
                case SceneTransitionHandler.SceneStates.Credits:
                    break;
                case SceneTransitionHandler.SceneStates.Death:
                    break;
                default:
                    throw new ArgumentException("No scene name found");
            }
        }
        actualScene = newScene;
    }
}
