using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Get bullet Transform component
    private Transform bulletTransform;

    // Get hitEffect animation
    public GameObject hitEffect;

    void Start()
    {
        // Get bullet Transform component
        bulletTransform = GetComponent<Transform>();
    }

    // Action when bullet hit something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If bullet hit something, log a message
        Debug.Log("Bullet hit something");

        // Instantiate hitEffect
        GameObject effect =
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        // Destroy hitEffect after 1 second
        Destroy(effect, 1f);

        // Destroy bullet
        Destroy (gameObject);
    }
}
