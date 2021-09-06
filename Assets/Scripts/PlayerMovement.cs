using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  // -------------------------------------------------
  // Player Components
  // --------------------------------------------------
  // Get Collider
  public Collider2D collider;

  // Get Rigidbody
  public Rigidbody2D rb;

  // Get Animator component
  public Animator animator;

  // Get Weapon Animator component
  // TODO: Add this in the inspector
  public Animator weaponAnimator;

  // Float for moveSpeed
  public float moveSpeed = 5f;

  // -------------------------------------------------
  // Assorted Variables
  // --------------------------------------------------
  // Vector2 for movement
  Vector2 movement;

  // -------------------------------------------------
  // Methods
  // --------------------------------------------------
  void Start()
  {
    // get the Collider2D component
    collider = GetComponent<Collider2D>();

    // get the Rigidbody2D component
    rb = GetComponent<Rigidbody2D>();

    // get the Animator component
    animator = GetComponent<Animator>();
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

    // If space is pressed
    if (Input.GetKeyDown(KeyCode.Space))
    {
      // Set trigger for "Sword_Attack_Right"
      weaponAnimator.SetTrigger("Sword_Attack_Right");
    }
  }

  void FixedUpdate()
  {
    // Move the player
    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
  }
}
