using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Float for moveSpeed
    public float moveSpeed = 5f;

    // add a Rigidbody2D for player in the inspector
    public Rigidbody2D rb;

    // Vector2 for movement
    Vector2 movement;

    void Start()
    {
        // get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // get the horizontal and vertical axis
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Create the movement vector and normalize it
        movement = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        // Move the player
        rb
            .MovePosition(rb.position +
            movement * moveSpeed * Time.fixedDeltaTime);
    }
}
