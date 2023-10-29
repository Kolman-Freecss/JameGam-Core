using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TriggerLocations : MonoBehaviour
{
    [SerializeField] private GameObject[] Triggers;
    [SerializeField] private TextMeshProUGUI textPlaces;

    [SerializeField] private Camera mainCamera;
    public float zoomSpeed = 1.5f;
    public float minZoom = 0f; // El nivel mínimo de zoom
    public float maxZoom = 0f; // El nivel máximo de zoom
    bool makeZoom = false;
    bool returnSchool = false;
    float finalZoom;

    #region Event Variables

    public event Action<bool> PhaseOneCompleted;

    #endregion

    void Update()
    {
        if (makeZoom)
        {
            Debug.Log("Entra");
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, Random.Range(minZoom, maxZoom),
                Time.deltaTime * zoomSpeed);
            if (mainCamera.orthographicSize == finalZoom) makeZoom = false;
        }
    }

    //TODO: Shit. Perform this :)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger TriggerLocation");
        // Verifica si el objeto que colision� tiene la etiqueta "Lugar"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Places"))
        {
            switch (collision.gameObject.name)
            {
                case "Room":
                    minZoom = mainCamera.orthographicSize;
                    maxZoom = mainCamera.orthographicSize;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "Main Zone":
                    if (!PlayerBehaviour.Instance.PhaseManager.phase1Completed)
                    {
                        PhaseOneCompleted?.Invoke(true);
                    }

                    minZoom *= 1f;
                    maxZoom *= 1.3f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "Street":
                    minZoom *= 1f;
                    maxZoom *= 1.3f;
                    //finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "School":
                    minZoom /= 1f;
                    maxZoom /= 1.3f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "Park":
                    if (!returnSchool)
                    {
                        minZoom *= 1f;
                        maxZoom *= 1.3f;
                        finalZoom = Random.Range(minZoom, maxZoom);
                        makeZoom = true;
                        returnSchool = true;
                    }

                    break;
                case "Road":
                    minZoom *= 1f;
                    maxZoom *= 1.3f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;

                    break;
                case "School1":
                    if (returnSchool)
                    {
                        minZoom /= 1f;
                        maxZoom /= 1.3f;
                        makeZoom = true;
                        returnSchool = false;
                    }
                    break;
                case "tomb":
                    Debug.Log("Win");
                    PlayerBehaviour.Instance.PhaseManager.WinGame();
                    break;
            }
            if (collision.gameObject.name != "School1")
            {
                // Actualiza el texto con el nombre del lugar
                textPlaces.text = collision.gameObject.name;
            }

        }
    }
}