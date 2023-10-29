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
    float finalZoom;

    #region Event Variables

    public event Action<bool> PhaseOneCompleted; 

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (makeZoom)
        {
            Debug.Log("Entra");
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, maxZoom, Time.deltaTime * zoomSpeed);
            if (mainCamera.orthographicSize == finalZoom) makeZoom = false;
        }

    }

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
                    minZoom *= 1f;
                    maxZoom *= 1.3f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "Road":
                    minZoom *= 1f;
                    maxZoom *= 1.3f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;

            }

            // Actualiza el texto con el nombre del lugar
            textPlaces.text = collision.gameObject.name;
        }
    }
}
