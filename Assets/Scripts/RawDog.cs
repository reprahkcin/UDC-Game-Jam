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

    void DisplayDetails()
    {
        details.SetActive(true);
        //Wait for 5 seconds
        StartCoroutine(HideDetails());
    }

    IEnumerator HideDetails()
    {
        yield return new WaitForSeconds(5);
        details.SetActive(false);
    }

    void Pickup()
    {


        // Display details
        DisplayDetails();

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



        // Destroy item
        Destroy(gameObject);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);

        // Restore player movement speed
        Player.instance.SetSpeed(Player.instance.moveSpeed * 2.0f);

        // Lower attack damage
        Player.instance.attackDamage -= 10;
    }
}
