using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Player
{
  // Get 2D Collider
  private BoxCollider2D swordCollider;

  void Start()
  {
    // Get 2D Collider
    swordCollider = GetComponent<BoxCollider2D>();
  }

  // On Collision with Enemy
  void OnTriggerEnter2D(Collider2D other)
  {
    // If Enemy
    if (other.gameObject.tag == "Enemy")
    {
      // Decrease Enemy Health
      other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }
  }
}
