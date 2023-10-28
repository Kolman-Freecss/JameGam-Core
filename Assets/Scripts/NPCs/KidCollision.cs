using System.Collections;
using UnityEngine;

public class KidCollision : MonoBehaviour
{
    public float speed = 5f; 
        
    Rigidbody2D rB;
    Vector2 direccionColision;
    bool collides = false;

    #region InitData

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    #endregion


    private void FixedUpdate()
    {
        //Debug.Log(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger");
            direccionColision = transform.position - other.transform.position;
            StartCoroutine(Flee(direccionColision));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        direccionColision = transform.position - other.transform.position;
        Vector2 direccionMove = new Vector2(-direccionColision.x, direccionColision.y - direccionColision.x / 2);
        StartCoroutine(Flee(direccionMove));
    }

    IEnumerator Flee(Vector3 direccion)
    {
        yield return new WaitForSeconds(0.2f); // Espera 300 milisegundos

        float tiempoInicio = Time.time; // Guarda el tiempo de inicio
        float tiempoMovimiento = 1f; // Define el tiempo de movimiento

        while (Time.time - tiempoInicio < tiempoMovimiento)
        {
            transform.Translate(direccion.normalized * speed *
                                Time.deltaTime); // Mueve el objeto en sentido contrario a la colisión
            yield return null; // Espera al siguiente frame
        }
    }
}