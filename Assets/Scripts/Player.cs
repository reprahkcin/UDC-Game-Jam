using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // -------------------------------------------------
    // GameObjects
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
    public int powerups = 5;

    // -------------------------------------------------
    // UI/Game Getters
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
            // Change the animation to Player_Idle_Down
            animator.SetBool("isMoving", false);



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

    public void SetPowerups(int powerups)
    {
        this.powerups = powerups;
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
    }
}
