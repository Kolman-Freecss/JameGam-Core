using System.Collections;
using UnityEngine;


public class KidCollision : MonoBehaviour
{
    public float speed = 5f; 
        
    Rigidbody2D rB;
    Animator an;
    Vector2 direccionColision;
    bool collides = false;
    bool running;
    [SerializeField] AudioClip[] audios = new AudioClip[3];
    AudioSource audioSource;
    float volume;

    #region InitData

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        an = GetComponent<Animator>();
        rB = GetComponent<Rigidbody2D>();
        audioSource.volume = volume;
        volume = PlayerPrefs.GetFloat("EffectsAudioPref");
    }

    #endregion

    private void Update()
    {
        Debug.Log(volume);
    }
    private void FixedUpdate()
    {
        //Debug.Log(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !running)
        {
            volume = PlayerPrefs.GetFloat("EffectsAudioPref");
            audioSource.volume = volume;
            audioSource.clip = audios[Random.Range(0, 3)];
            audioSource.Play();
            running = true;
            Debug.Log("Trigger");
            an.SetBool("Walk", true);
            direccionColision = transform.position - other.transform.position;
            StartCoroutine(Flee(direccionColision));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            running = false;
            an.SetBool("Walk", false);
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