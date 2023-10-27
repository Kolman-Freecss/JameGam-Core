using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rB;
    Vector2 inputMovement;
    [SerializeField] float speed = 10;
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    private void FixedUpdate()
    {
        rB.AddRelativeForce(new Vector2(inputMovement.x * speed, inputMovement.y * speed), ForceMode2D.Impulse);
    }
}   