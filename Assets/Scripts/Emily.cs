using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emily : MonoBehaviour
{
    // Singleton
    public static Emily instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Door animator
    public GameObject door;


    // Trigger Animation to Give_Item
    public void GiveItem()
    {
        // Set the bool isOpen to true on door animator
        door.GetComponent<Animator>().SetTrigger("OpenDoor");


        // Play the sound
        SoundManager.instance.PlayDoorOpening();

        // Wait for door to open
        StartCoroutine(Wait(1.5f));


    }

    // Wait for door to open
    IEnumerator Wait(float time)
    {
        // yield return new WaitForSeconds(time);

        GetComponent<Animator>().SetTrigger("Give_Item");



        // wait for animation to finish
        yield return new WaitForSeconds(3);

        // close the door
        door.GetComponent<Animator>().SetTrigger("CloseDoor");

        // Play the sound
        SoundManager.instance.PlayDoorClosing();
    }
}
