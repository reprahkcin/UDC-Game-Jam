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


    // Spawn Point
    public Transform spawnPoint;

    // -------------------------------------------------
    // Stats
    // -------------------------------------------------

    // Max Health
    public int maxHealth = 100;

    // Health
    public int health = 100;

    // Attack Damage
    public int attackDamage = 15;

    // Float for moveSpeed
    public float moveSpeed = 5f;

    // Poison Pickle Damage
    public int poisonDamage = 100;

    // Poison Pickle Duration
    public float poisonDuration = 5f;

    // Poison Pickle Boolean
    public bool isPoisoned = false;

    // Bool isAlive
    private bool isAlive = true;

    // Number Help Requests
    public int helpRequests = 5;

    // -------------------------------------------------
    // UI/Game Getters
    // -------------------------------------------------
    // Health
    public float GetHealth()
    {
        // Return current health as a percentage
        return (float)health / (float)maxHealth;

    }



    // Help Requests
    public int GetHelpRequests()
    {
        // Return current help requests
        return helpRequests;
    }

    // Set Speed
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetAttackDamage(int damage)
    {
        attackDamage = damage;
    }

    public void addHelpRequest()
    {
        // Set new help requests
        helpRequests++;
    }

    // Set help requests
    public void SetHelpRequests(int requests)
    {
        // Set new help requests
        helpRequests = requests;
    }



    // Check if player is alive
    public bool isAliveBool()
    {
        return isAlive;
    }


    // -------------------------------------------------
    // Interacting with Enemies
    // -------------------------------------------------

    public void ResetPlayer()
    {
        // Reset player stats
        health = maxHealth;
        isPoisoned = false;
        isAlive = true;
        helpRequests = 5;

        // Reset player position
        transform.position = spawnPoint.position;

        // Reset player animation
        animator.SetBool("isMoving", false);

        // Set player to simulated
        GetComponent<Rigidbody2D>().simulated = true;


    }


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


            // Set isDying to true
            isDying = true;

            // Start the Death Sequence. Most will take place here, but the rat swarm will be handled in the GameManager script
            Death();
        }
    }

    public void AddHealth(int healthToAdd)
    {
        // Add healthToAdd to health
        health += healthToAdd;

        // If health is more than 100, set it to 100
        if (health > 100)
        {
            health = 100;
        }
    }


    public void DamageFlash()
    {
        // Set Player spriteRenderer color to red
        GetComponent<SpriteRenderer>().color = Color.red;

        // Play a hurt sound
        SoundManager.instance.PlayHurt();

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


    public bool isDying = false;
    // Player death
    public void Death()
    {
        // Set isAlive to false
        // This should stop movement and attack capabilities, as they both check
        isAlive = false;

        // Rigiddbody isSimulated to false
        GetComponent<Rigidbody2D>().simulated = false;

        // Change the animation to Player_Idle_Down
        animator.SetBool("isMoving", false);

        // Trigger death animation
        animator.SetTrigger("isDead");

        // Change the sorting order of the player to 1
        GetComponent<SpriteRenderer>().sortingOrder = 1;

        // Stop the theme1 music
        SoundManager.instance.StopTheme1();


        if (isDying)
        {
            // Play the death sound
            SoundManager.instance.PlayDeath1();

            // set isDying to false
            isDying = false;
        }

        // Wait for 4 seconds
        StartCoroutine(DeathTimer());


        // TODO: Death Animation
    }

    IEnumerator DeathTimer()
    {
        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Load last canvas
        CanvasManager.instance.SetCanvas(3);
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
                // play a grunt sound
                SoundManager.instance.PlayGrunt();

                // Play a whoosh Sound
                SoundManager.instance.PlayWhoosh();

                // Set weapon animator trigger to attack
                weaponAnimator.SetTrigger("Attack");
                animator.SetTrigger("Attack");

                StartCoroutine(SwingTimer());

            }
        }
    }

    IEnumerator SwingTimer()
    {
        // Wait for 0.01 seconds
        yield return new WaitForSeconds(0.01f);

        // Play shovelSound
        SoundManager.instance.PlayShovel();
    }
}
