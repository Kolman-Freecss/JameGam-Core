using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, Random.Range(minZoom, maxZoom), Time.deltaTime * zoomSpeed);
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
                    break;
                case "Main Zone":
                    minZoom *= 1f;
                    maxZoom *= 2f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "Street":
                    minZoom *= 1f;
                    maxZoom *= 2f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "School":
                    minZoom /= 1f;
                    maxZoom /= 2f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "Park":
                    minZoom *= 1f;
                    maxZoom *= 2f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;
                case "Road":
                    minZoom *= 1f;
                    maxZoom *= 2f;
                    finalZoom = Random.Range(minZoom, maxZoom);
                    makeZoom = true;
                    break;

            }

            // Actualiza el texto con el nombre del lugar
            textPlaces.text = collision.gameObject.name;
        }
    }
}
