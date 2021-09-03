using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCollision : MonoBehaviour
{
    // Get the BoxCollider component
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Collider component
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // If collision occurs, Debug.Log the details of the collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the name of this object
        string name = gameObject.name;
        Debug.Log(name + "collided with " + collision.gameObject.name);
    }
}
