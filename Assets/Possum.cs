using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possum : MonoBehaviour
{
    // -------------------------------------------------------------
    // GameObjects
    // -------------------------------------------------------------

    // rigidbody
    public Rigidbody2D rb;

    // Health Bar Canvas
    public RectTransform healthBarCanvas;

    // Health Bar GameObject
    public GameObject healthBar;

    // -------------------------------------------------------------
    // Possum Stats
    // -------------------------------------------------------------

    // Max Health
    public float maxHealth = 100;

    // Possum Health
    public int health = 100;

    // Possum Speed
    public float speed = 1.0f;

    // Possum Damage
    public int damage = 10;

    // Possum Attack Range
    public float attackRange = 1.0f;
    // -------------------------------------------------------------
    // Unity Methods
    // -------------------------------------------------------------

    void FixedUpdate()
    {
        // Possum Movement
        PossumWalk();
        PossumPursue();
    }


    // -------------------------------------------------------------
    // Possum Behaviors
    // -------------------------------------------------------------

    // Possum Walk
    public void PossumWalk()
    {
        // Continuously moves the possum forward
        rb.velocity = transform.up * speed;

    }

    void PossumWander()
    {
        // Turns the possum 15 degrees one way or the other every second


        // Get the current direction of the Possum
        Vector3 currentDirection = transform.position - transform.position;


        // Generate a random turn amount
        float turnAmount = Random.Range(-15.0f, 15.0f);

        // Turn possum by the turn amount
        rb.MoveRotation(rb.rotation + turnAmount);
    }

    void PossumPursue()
    {
        Vector3 direction = Player.instance.GetComponent<Transform>().position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;

        // Get Enemy local rotation
        Vector3 localRotation = transform.localEulerAngles;

        //
        // Rotate the healthBarCanvas to always stay upright
        healthBarCanvas.localEulerAngles = new Vector3(0, 0, -(localRotation.z));
    }


    IEnumerator PossumAlgorithm()
    {
        while (true)
        {
            // Always be Walkin'
            PossumWalk();

            // Get the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, Player.instance.transform.position);

            // If the distance is less than the attack range
            if (distanceToPlayer < attackRange)
            {
                // Attack
                PossumPursue();
            }
            else
            {
                // Wander
                PossumWander();
            }

            // Wait for a second
            yield return new WaitForSeconds(1.0f);
        }
    }

    // ------------------------------------------------------------------------
    // Health Bar
    // ------------------------------------------------------------------------

    // Update Health Bar
    public void UpdateHealthBar()
    {
        // Get the RectTransform of the healthBar
        RectTransform healthBarRect = healthBar.GetComponent<RectTransform>();

        // Get the ratio of health to max health
        float ratio = health / maxHealth;

        // Set HealthBarObject's local scale x to health
        healthBarRect.localScale = new Vector3(ratio, 1, 1);
    }
}
