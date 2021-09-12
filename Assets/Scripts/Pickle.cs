using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickle : MonoBehaviour
{

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
        // Display the details
        CanvasManager.instance.ActivatePickleDetails();

        // Play Eating sound
        SoundManager.instance.PlayEating();

        // Instantiate pickup effect
        Instantiate(pickupEffect, transform.position, transform.rotation);


        // Start Poison Pickle Effect
        Player.instance.GetComponent<Player>().PoisonPickle();

        // Destroy item
        Destroy(gameObject);
    }
}
