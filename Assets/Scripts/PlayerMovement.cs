using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // -------------------------------------------------
    // Player Components
    // --------------------------------------------------

    // Get Rigidbody
    private Rigidbody2D rb;

    // Get Animator component
    private Animator animator;

    // Weapon Animator
    public Animator weaponAnimator;

    // Float for moveSpeed
    public float moveSpeed = 5f;

    // Player script
    private Player player;

    // -------------------------------------------------
    // Assorted Variables
    // --------------------------------------------------

    // Vector2 container for calculated movement
    Vector2 movement;

    // -------------------------------------------------
    // Unity Methods
    // --------------------------------------------------
    void Start()
    {
        // get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // get the Animator component
        animator = GetComponent<Animator>();

        // get the Player script
        player = GetComponent<Player>();

    }

    void FixedUpdate()
    {
        // If the player is still alive, apply the movemement created below
        if (player.isAliveBool())
        {
            // Move the player
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        // -------------------------------------------------
        // Movement Controls
        // --------------------------------------------------
        // If the player is still alive
        if (player.isAliveBool())
        {
            // get the horizontal and vertical axis
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            // Create the movement vector and normalize it
            movement = new Vector2(x, y).normalized;

            // Set speed animator parameter to movement magnitude
            animator.SetFloat("Speed", movement.magnitude);




            // If x is less than 0
            if (x < 0)
            {
                animator.SetFloat("Horizontal", -1f);
                animator.SetFloat("Vertical", 0f);
                weaponAnimator.SetFloat("dirX", -1f);
                weaponAnimator.SetFloat("dirY", 0f);
            } // If x is greater than 0
            else if (x > 0)
            {
                animator.SetFloat("Horizontal", 1f);
                animator.SetFloat("Vertical", 0f);
                weaponAnimator.SetFloat("dirX", 1f);
                weaponAnimator.SetFloat("dirY", 0f);
            } // If y is less than 0
            if (y < 0)
            {
                animator.SetFloat("Vertical", -1f);
                animator.SetFloat("Horizontal", 0f);
                weaponAnimator.SetFloat("dirY", -1f);
                weaponAnimator.SetFloat("dirX", 0f);
            } // If y is greater than 0
            else if (y > 0)
            {
                animator.SetFloat("Vertical", 1f);
                animator.SetFloat("Horizontal", 0f);
                weaponAnimator.SetFloat("dirY", 1f);
                weaponAnimator.SetFloat("dirX", 0f);
            }

            // If the float speed is greater than 0
            if (movement.magnitude > 0)
            {
                // set animator bool isMoving to true
                animator.SetBool("isMoving", true);
            }
            else
            {
                // set animator bool isMoving to false
                animator.SetBool("isMoving", false);
            }
        }
    }

}
