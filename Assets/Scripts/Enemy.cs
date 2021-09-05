using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // Enemy Stats
    // ------------------------------------------------------------------------

    // Enemy Health
    public int health = 100;

    // Enemy Damage
    public int damage = 10;

    // Enemy Speed
    public float speed = 10f;

    // ------------------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------------------

    // Get Enemy Damage
    public int GetDamage()
    {
        return damage;
    }

    // Take damage - Called from player
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy (gameObject);
    }

    void Update()
    {
        // If health is less than 0, destroy the enemy
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
