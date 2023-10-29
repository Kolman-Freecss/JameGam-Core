using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeRope : MonoBehaviour
{
    [SerializeField] private GameObject rope;
    [SerializeField] private GameObject takepanel;

    // Start is called before the first frame update
    void Start()
    {
        takepanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            takepanel.SetActive(false);
            if (rope.CompareTag("Rope"))
            {
                Destroy(rope);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        takepanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        takepanel.SetActive(false);
    }
}
