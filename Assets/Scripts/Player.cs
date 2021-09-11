using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ------------------------------------------------
    // Singleton
    // ------------------------------------------------

    public static Player instance;


    // -------------------------------------------------
    // GameObjects and Components
    // -------------------------------------------------

    // Weapon Animator
    // TODO: Add this in the inspector
    public Animator weaponAnimator;

    // Animator
    public Animator animator;

    // -------------------------------------------------
    // Stats
    // -------------------------------------------------

    // Max Health
    public int maxHealth = 100;

    // Health
    public int health = 100;

    // Attack Damage
    public int attackDamage = 15;

    // Poison Pickle Damage
    public int poisonDamage = 100;

    // Poison Pickle Duration
    public float poisonDuration = 5f;

    // Poison Pickle Boolean
    public bool isPoisoned = false;

    // Bool isAlive
    private bool isAlive = true;

    // Number of Hotdogs Available
    public int hotdogs = 5;

    // -------------------------------------------------
    // UI/Game Getters
    // -------------------------------------------------
    // Health
    public float GetHealth()
    {
        // Return current health as a percentage
        return (float)health / (float)maxHealth;

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

    public void PoisonPickle()
    {
        // Set isPoisoned to true
        isPoisoned = true;

        // Set Player spriteRenderer color to green
        GetComponent<SpriteRenderer>().color = Color.green;

        Debug.Log("Player is poisoned");

        // Wait for poisonDuration
        StartCoroutine(PoisonPickleTimer());
    }

    IEnumerator PoisonPickleTimer()
    {
        // Wait for poisonDuration
        yield return new WaitForSeconds(poisonDuration);

        // Set Player spriteRenderer color to white
        GetComponent<SpriteRenderer>().color = Color.white;

        // Set isPoisoned to false
        isPoisoned = false;
    }


    public void DamagePlayer(int damage)
    {
        // Subtract damage from health
        health -= damage;

        // Loop DamageFlash() 10 times
        for (int i = 0; i < 10; i++)
        {
            // DamageFlash()
            DamageFlash();
        }

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

    public void DamageFlash()
    {
        // Set Player spriteRenderer color to red
        GetComponent<SpriteRenderer>().color = Color.red;

        // Wait for 0.1 seconds
        StartCoroutine(DamageFlashTimer());

    }

    IEnumerator DamageFlashTimer()
    {
        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Set Player spriteRenderer color to white
        GetComponent<SpriteRenderer>().color = Color.white;
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

        //GameManager.instance.DeathSwarm();

        // TODO: Death Animation
    }


    // -------------------------------------------------
    // Unity Methods
    // -------------------------------------------------
    //
    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
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

            // If the player is alive
            if (isAlive)
            {

                // Play the Whoosh2 Sound
                SoundManager.instance.PlaySound("Whoosh2");

                // Set weapon animator trigger to attack
                weaponAnimator.SetTrigger("Attack");
                animator.SetTrigger("Attack");

            }
        }
    }
}
