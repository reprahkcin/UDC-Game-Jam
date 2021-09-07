using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    // Get 2D Collider
    private BoxCollider2D swordCollider;

    // Damage the sword does
    public int swordDamage = 50;

    // Knock back force
    public float knockBackForce = 10f;

    void Start()
    {
        // Get 2D Collider
        swordCollider = GetComponent<BoxCollider2D>();
    }

    // On Collision with Enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with " + other.name);
        // If Enemy
        if (other.gameObject.tag == "Enemy")
        {
            // Decrease Enemy Health
            other.gameObject.GetComponent<Enemy>().DamageEnemy(swordDamage);

            // knock back enemy
            other.gameObject.GetComponent<HuntPlayer>().isAttacked = true;
        }
    }
}
