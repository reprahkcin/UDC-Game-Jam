using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // -------------------------------------------------
    // GameObjects and Components
    // -------------------------------------------------

    // GameManager
    private GameManager gameManager;

    // Weapon Animator
    // TODO: Add this in the inspector
    public Animator weaponAnimator;

    // Animator
    private Animator animator;

    // Player movement script
    private PlayerMovement playerMovement;


    // -------------------------------------------------
    // Stats
    // -------------------------------------------------
    // Health
    public int health = 100;

    // Bool isAlive
    private bool isAlive = true;

    // Number of Hotdogs Available
    public int hotdogs = 5;

    // -------------------------------------------------
    // UI/Game Getters
    // -------------------------------------------------
    // Health
    public int GetHealth()
    {
        return health;
    }

    // Hotdogs
    public int GetHotdogs()
    {
        return hotdogs;
    }

    // Set Hotdogs
    public void SetHotdogs(int newHotdogs)
    {
        hotdogs = newHotdogs;
    }

    // Check if player is alive
    public bool isAliveBool()
    {
        return isAlive;
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
        // If health is less than 0, set it to 0
        else if (health < 0)
        {
            health = 0;
        }

        // Check if health is less than 0
        if (health <= 0)
        {
            // Inform the family that the player is dead
            Debug.Log("He ded.");

            // Start the Death Sequence. Most will take place here, but the rat swarm will be handled in the GameManager script
            Death();
        }
    }

    // Player death
    public void Death()
    {
        // Set isAlive to false
        // This should stop movement and attack capabilities, as they both check
        isAlive = false;

        // Change the animation to Player_Idle_Down
        animator.SetBool("isMoving", false);

        // Relase the swarm of rats
        //gameManager.DeathSwarm();

        //GameManager.gm.DeathSwarm();

        // TODO: Death Animation
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

        // Get the GameManager
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        // -------------------------------------------------
        // Mouse Controls
        // --------------------------------------------------

        // If mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // If the player is alive
            if (isAlive)
            {
                // Set weapon animator trigger to attack
                weaponAnimator.SetTrigger("Attack");
                animator.SetTrigger("Attack");
            }
        }
    }
}
