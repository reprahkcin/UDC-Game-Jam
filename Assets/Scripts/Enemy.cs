using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // GameObjects
    // ------------------------------------------------------------------------

    // GameManager
    private GameManager gameManager;

    // SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // Dead sprite
    // TODO: Add this in the inspector
    public Sprite deadSprite;

    // Hunt Player script
    private HuntPlayer huntPlayer;

    // Enemy Rigidbody 2D
    private Rigidbody2D rb;

    // Enemy Animator
    private Animator animator;

    // ------------------------------------------------------------------------
    // Enemy Stats
    // ------------------------------------------------------------------------

    // Enemy Health
    private int health = 50;

    // Point Value
    public int pointValue = 10;

    // ------------------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------------------

    // Get Enemy Health
    public int GetHealth()
    {
        return health;
    }

    // Take damage - Called from player
    public void DamageEnemy(int damage)
    {
        health -= damage;
    }

    // Knock Enemy Back
    // TODO: Calculate knockback opoosite of point of impact
    public void KnockBackEnemy(float knockBackForce)
    {

        rb.AddForce(new Vector2(knockBackForce, 0));
    }

    // ------------------------------------------------------------------------
    // Unity Methods
    // ------------------------------------------------------------------------

    private void Start()
    {
        // Get SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get HuntPlayer script
        huntPlayer = GetComponent<HuntPlayer>();

        // Get Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Get GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Get Animator
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (health <= 0)
        {
            // Call ToggleMovement() on HuntPlayer script
            huntPlayer.ToggleMovement();
            // Start DeathTimer coroutine
            StartCoroutine(DeathTimer(3f));
        }

    }

    // ------------------------------------------------------------------------
    // Timers
    // ------------------------------------------------------------------------

    // Timer for death
    IEnumerator DeathTimer(float time)
    {
        // Change the sprite to dead
        spriteRenderer.sprite = deadSprite;

        // Trigger isDead animation
        animator.SetTrigger("isDead");

        // Update score on GameManager
        gameManager.UpdateScore(pointValue);

        // Wait for death
        yield return new WaitForSeconds(time);


        // Destroy gameObject
        Destroy(gameObject);
    }

}
