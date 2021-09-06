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

        // Check if health is less than 0
        if (health <= 0)
        {
            Debug.Log("You died!");
        }
    }

}
