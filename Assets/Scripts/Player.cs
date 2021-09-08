using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // -------------------------------------------------
    // GameObje
    // -------------------------------------------------

    // GameManager
    public GameManager gameManager;

    // Weapon Animator
    // TODO: Add this in the inspector
    public Animator weaponAnimator;

    // Animator
    private Animator animator;


    // -------------------------------------------------
    // Stats
    // -------------------------------------------------
    // Health
    private int health = 100;

    // Player movement script
    private PlayerMovement playerMovement;

    // Amount of powerups available
    private int powerups = 5;

    // -------------------------------------------------
    // UI Getters
    // -------------------------------------------------
    // Health
    public int GetHealth()
    {
        return health;
    }



    // -------------------------------------------------
    // Interacting with Enemies
    // -------------------------------------------------

    public void DamagePlayer(int damage)
    {
        // Subtract damage from health
        health -= damage;

        // If health is more than 100, set it to 100
        if (health > 100)
        {
            health = 100;
        }

        // Check if health is less than 0
        if (health <= 0)
        {
            playerMovement.Death();
            Debug.Log("You died!");
            GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDeath();
        }
    }

    // -------------------------------------------------
    // Items
    // -------------------------------------------------

    // Get powerups available
    public int GetPowerups()
    {
        return powerups;
    }

    // Add powerup
    public void AddPowerup()
    {
        powerups++;
    }

    // Remove powerup
    public void RemovePowerup()
    {
        powerups--;
    }




    // -------------------------------------------------
    // Unity Methods
    // -------------------------------------------------
    //
    void Start()
    {
        // Get the player movement script
        playerMovement = GetComponent<PlayerMovement>();


        // Get the animator
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // -------------------------------------------------
        // Mouse Controls
        // --------------------------------------------------


        // If mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Set weapon animator trigger to attack
            weaponAnimator.SetTrigger("Attack");
            animator.SetTrigger("Attack");
        }

        // If right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            // if remaining powerups is greater than 0
            if (powerups > 0)
            {
                // Spawn an item
                gameManager.GetComponent<GameManager>().SpawnItem();
                // Decrease remaining powerups by 1
                powerups--;
            }
        }
    }
}
