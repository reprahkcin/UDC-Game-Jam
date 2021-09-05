using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy Health
    public int health = 100;

    // Enemy Damage
    public int damage = 10;

    // Enemy Speed
    public float speed = 10f;

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
}
