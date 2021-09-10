using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // -------------------------------------------------
    // GameObjects
    // -------------------------------------------------

    // Player
    private GameObject player;

    // -------------------------------------------------
    // Variables
    // -------------------------------------------------

    // Attack Damage
    private int attackDamage = 1;

    // -------------------------------------------------
    // Functions
    // -------------------------------------------------

    // Damage Player
    public void DamagePlayer()
    {
        // Damage player
        player.GetComponent<Player>().DamagePlayer(attackDamage);
    }

    // -------------------------------------------------
    // Unity Functions
    // -------------------------------------------------

    void Start()
    {
        // Set player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If player
        if (other.gameObject.tag == "Player")
        {
            // Damage player
            DamagePlayer();
        }
    }

}
