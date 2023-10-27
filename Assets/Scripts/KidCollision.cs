using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidCollision : MonoBehaviour
{
    Rigidbody2D rB;
    Vector2 direccionColision;
    bool collides = false;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Debug.Log(transform.position);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            direccionColision = transform.position - other.transform.position;
            StartCoroutine(MoverContra(direccionColision));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        direccionColision = transform.position - other.transform.position;
        Vector2 direccionMove = new Vector2(-direccionColision.x, direccionColision.y - direccionColision.x / 2);
        StartCoroutine(MoverContra(direccionMove));
    }

    IEnumerator MoverContra(Vector3 direccion)
    {
        yield return new WaitForSeconds(0.2f); // Espera 300 milisegundos

        float tiempoInicio = Time.time; // Guarda el tiempo de inicio
        float tiempoMovimiento = 1f; // Define el tiempo de movimiento

        while (Time.time - tiempoInicio < tiempoMovimiento)
        {
            transform.Translate(direccion.normalized * 30 * Time.deltaTime); // Mueve el objeto en sentido contrario a la colisión
            yield return null; // Espera al siguiente frame
        }
    }

}
