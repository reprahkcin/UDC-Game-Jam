using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Health
    private int health = 100;

    public void SetHealth(int health)
    {
        this.health = health;
    }

    // Get Collider
    public Collider2D collider;

    // Float for moveSpeed
    public float moveSpeed = 5f;

    // add a Rigidbody2D for player in the inspector
    public Rigidbody2D rb;

    // Get Animator component
    public Animator animator;

    // Vector2 for movement
    Vector2 movement;

    // Getter and Sfor health
    void Start()
    {
        // get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // get the Animator component
        animator = GetComponent<Animator>();

        // get the Collider2D component
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // get the horizontal and vertical axis
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Create the movement vector and normalize it
        movement = new Vector2(x, y).normalized;

        // Set horizontal animator parameter to horizontal movement
        animator.SetFloat("Horizontal", movement.x);

        // Set vertical animator parameter to vertical movement
        animator.SetFloat("Vertical", movement.y);

        // Set speed animator parameter to movement magnitude
        animator.SetFloat("Speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        // Move the player
        rb
            .MovePosition(rb.position +
            movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Hot Dog")
        {
            // Add health
            this.health += other.gameObject.GetComponent<PowerUp>().healthBuff;

            // Destroy hot dog
            Destroy(other.gameObject);
        }
    }
}
