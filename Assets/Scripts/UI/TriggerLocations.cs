using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerLocations : MonoBehaviour
{
    [SerializeField] private GameObject [] Triggers;
    [SerializeField] private TextMeshProUGUI textPlaces;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("entro al trigger");
        // Verifica si el objeto que colisionó tiene la etiqueta "Lugar"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Places"))
        {
            // Actualiza el texto con el nombre del lugar
            textPlaces.text = collision.gameObject.name;
        }
    }
}
