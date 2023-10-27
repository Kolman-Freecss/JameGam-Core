using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinoChoca : MonoBehaviour
{
    


    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() 
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            //TODO: Hacer el movimiento al contrario del player
            float offset = other.contactOffset;
            Debug.Log(offset);
        }
        else
        {
            //TODO: Hacer el movimiento al contrario de los obstaculos

        }
    }
}