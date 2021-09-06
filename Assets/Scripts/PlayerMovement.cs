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
    }

    // If y is less than 0
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

    // If mouse button is pressed
    if (Input.GetMouseButtonDown(0))
    {
      // Set weapon animator trigger to attack
      weaponAnimator.SetTrigger("Attack");
      animator.SetTrigger("Attack");
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

  void FixedUpdate()
  {
    // Move the player
    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
  }
}
