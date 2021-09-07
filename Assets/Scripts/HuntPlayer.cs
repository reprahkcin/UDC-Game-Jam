using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntPlayer : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // GameObject Connections
    // ------------------------------------------------------------------------

    // Declare Player Transform
    private Transform player;

    // Rigidbody of the rat
    public Rigidbody2D rb;

    // Enemy script
    private Enemy enemy;

    // ------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------

    // Speed of the Enemy
    private float speed = 1.0f;

    //  Min-Max Range for enemy speed
    [Range(0.0f, 2.0f)]
    public float minSpeed = 1.0f;

    [Range(0.0f, 2.0f)]
    public float maxSpeed = 1.0f;

    // bool isMoving
    public bool isMoving = true;

    public bool isAttacked = false;

    // ------------------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------------------

    // Toggles the isMoving bool
    public void ToggleMovement()
    {
        isMoving = !isMoving;
    }

    // Get the direction of the player and rotate the rigidbody to face it
    private void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;
    }

    // ------------------------------------------------------------------------
    // Unity Methods
    // ------------------------------------------------------------------------

    void Start()
    {
        // Use minSpeed and MaxSpeed to generate a random speed for the enemy
        speed = Random.Range(minSpeed, maxSpeed);

        // Get the player position
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the rat's rigidbody
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // If the enemy is moving
        if (isMoving && !isAttacked)
        {
            // Face the player
            FacePlayer();
            // Move Enemy forward at speed
            rb.velocity = transform.up * speed;
        }
        else if (isMoving && isAttacked)
        {
            rb.velocity = transform.up * -30;
            // Start coroutine to reset isAttacked
            StartCoroutine(Wait());
        }

        // If the enemy is not moving
        else if (!isMoving)
        {
            // Make rigidbody not simulated
            rb.simulated = false;

            // Set the sorting order to 1
            GetComponent<SpriteRenderer>().sortingOrder = 1;

        }
    }

    // ------------------------------------------------------------------------
    // Timer
    // ------------------------------------------------------------------------

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacked = false;
    }
}
