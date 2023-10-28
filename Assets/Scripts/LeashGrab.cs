using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeashGrab : MonoBehaviour
{
    [SerializeField] GameObject leash;
    Collider2D coll;
    Vector2 mousePos;
    float angle;
    bool grabbing;
    void Start()
    {
        leash.SetActive(false);
        coll = leash.GetComponent<Collider2D>();
    }
    void Update()
    {
        mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //angle = mousePos.x / mousePos.y;
        angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        if (Input.GetMouseButtonDown(0) && !grabbing)
        {
            grabbing = true;
            leash.SetActive(true);
            transform.eulerAngles = new Vector3(0, 0, -angle +90);
            StartCoroutine(LeashGrabbing());
        }
    }
    IEnumerator LeashGrabbing()
    {
        yield return new WaitForSeconds(1);
        leash.SetActive(false);
        grabbing = false;
    }
}
