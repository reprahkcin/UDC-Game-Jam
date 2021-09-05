using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatWander : MonoBehaviour
{
    // Player position
    public Transform player;

    // Speed of the rat
    public float speed = 1.0f;

    // Rigidbody of the rat
    private Rigidbody2D rb;

    void Start()
    {
        // Get the player position
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the rat's rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get the direction to the player
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;

        // Move rat forward at speed
        rb.velocity = transform.up * speed;
    }
}
