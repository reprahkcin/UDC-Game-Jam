using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawDog : MonoBehaviour
{
    public GameObject pickupEffect;

    public GameObject details;

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



        // Display details
        CanvasManager.instance.ActivateRawDogDetails();

        // Play Eating sound
        SoundManager.instance.PlayEating();

        // Instantiate pickup effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // raise health
        Player.instance.AddHealth(15);

        // Raise attack damage
        Player.instance.attackDamage += 10;

        // Temporarily reduce player movement speed
        Player.instance.SetSpeed(Player.instance.moveSpeed * 0.5f);

        // Wait for 5 seconds
        StartCoroutine(Wait());

        // Set self to inactive
        gameObject.SetActive(false);


    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);

        // Restore player movement speed
        Player.instance.SetSpeed(5f);

        // Lower attack damage
        Player.instance.attackDamage -= 10;


        // Destroy item
        Destroy(gameObject);
    }
}
