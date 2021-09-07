using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // -------------------------------------------------
    // Stats
    // -------------------------------------------------
    // Health
    private int health = 100;

    // Player movement script
    private PlayerMovement playerMovement;

    // -------------------------------------------------
    // UI Getters
    // -------------------------------------------------
    // Health
    public int GetHealth()
    {
        return health;
    }



    // -------------------------------------------------
    // Collecting and Using Items
    // -------------------------------------------------
    // TODO: Items need to have a 2D Collider and isTrigger set to true
    void OnTriggerEnter2D(Collider2D other)
    {
        // Console log the trigger
        //Debug.Log("Trigger: " + other.name);

        // Check if the trigger is an item
        if (other.CompareTag("Item"))
        {
            // Get the Item script
            Item item = other.GetComponent<Item>();

            // Apply health buff
            health += item.getHealthBuff();

            // Destroy the item
            Destroy(other.gameObject);
        }
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
    // Unity Methods
    // -------------------------------------------------
    //
    void Start()
    {
        // Get the player movement script
        playerMovement = GetComponent<PlayerMovement>();
    }
}
