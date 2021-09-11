using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDog : MonoBehaviour
{
    // Health to be added to player
    public int health;


    public GameObject pickupEffect;


    // 2d trigger enter
    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.tag == "Consumer")
        {
            Pickup();
        }
    }
    void Pickup()
    {
        // Play Eating sound
        SoundManager.instance.PlayEating();

        // Instantiate pickup effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // Find the player script
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // Add health
        player.DamagePlayer(-health);

        // Destroy item
        Destroy(gameObject);
    }
}
