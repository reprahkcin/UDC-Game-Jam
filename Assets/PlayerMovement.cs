using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Camera cam;

    Vector2 movement;

    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical axis
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Get current position of mouse
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Move the player
        rb
            .MovePosition(rb.position +
            movement * moveSpeed * Time.fixedDeltaTime);

        //Restrict player to camera
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        transform.position = cam.ViewportToWorldPoint(viewPos);

        // Look at the mouse
        Vector2 lookDir = mousePos - rb.position;

        // Get angle between player and mouse
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // Rotate player
        rb.rotation = angle;
    }
}
