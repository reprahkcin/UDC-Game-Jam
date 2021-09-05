using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Get the fire point
    public Transform firePoint;

    // Get the bullet prefab
    public GameObject bulletPrefab;

    // Get the bullet speed
    public float bulletSpeed = 20f;

    // public float to control bullet size
    [Range(0.1f, 1.0f)]
    public float bulletSize = 0.1f;

    void Update()
    {
        // If the player presses 'fire1'
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create new Vector3 to set bullet size
        Vector3 bulletSizeVector =
            new Vector3(bulletSize, bulletSize, bulletSize);

        // Create a new bullet
        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's size
        bullet.transform.localScale = bulletSizeVector;

        // Get the Rigidbody2D component
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Add force to the bullet
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
