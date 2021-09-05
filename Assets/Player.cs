using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // -------------------------------------------------
    // GameObject Connections
    // TODO: Add this in the inspector
    // -------------------------------------------------
    // Get Collider for weapon
    public Collider weaponCollider;


    // -------------------------------------------------
    // Stats
    // -------------------------------------------------
    // Health
    public int health = 100;

    // Damage
    public int damage = 10;

    // Speed
    public float speed = 5f;

    // defense
    public int defense = 10;

    // -------------------------------------------------
    // Collecting and Using Items
    // -------------------------------------------------
    // TODO: Items need to have a 2D Collider and isTrigger set to true
    void OnTriggerEnter2D(Collider2D other)
    {
        // Console log the trigger
        Debug.Log("Trigger: " + other.name);

        // Check if the trigger is an item
        if (other.CompareTag("Item"))
        {
            // Get the Item script
            Item item = other.GetComponent<Item>();
            
            // Apply health buff
            health += item.getHealthBuff();

            // Apply damage buff
            damage += item.getDamageBuff();

            // Apply speed buff
            speed += item.getSpeedBuff();

            // Apply defense buff
            defense += item.getDefenseBuff();

            // Destroy the item
            Destroy(other.gameObject);
        }

    }

    // -------------------------------------------------
    // Interacting with Enemies
    // -------------------------------------------------
    void OnCollisionEnter2D(Collision2D collision)
    {

        // Console log the collision
        Debug.Log("Collision: " + collision.gameObject.name);

        // Check if the collision is an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the Enemy script
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Get the enemy's damage
            int enemyDamage = enemy.GetDamage();

            // Subtract defense percentage from enemyDamage
            enemyDamage -= (enemyDamage * defense / 100);

            // Apply damage to health
            health -= enemyDamage;

        }
    }

}
