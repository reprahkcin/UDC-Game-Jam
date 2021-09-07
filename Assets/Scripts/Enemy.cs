using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // GameObjects
    // ------------------------------------------------------------------------

    // SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // Dead sprite
    // TODO: Add this in the inspector
    public Sprite deadSprite;

    // GameManager
    private GameManager gameManager;

    // Hunt Player script
    public HuntPlayer huntPlayer;

    // Enemy Rigidbody 2D
    private Rigidbody2D rb;

    // Animator
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
            StartCoroutine(DeathTimer());
        }

    }

    // ------------------------------------------------------------------------
    // Timers
    // ------------------------------------------------------------------------

    // Timer for death
    IEnumerator DeathTimer()
    {


        // Change the sprite to dead
        spriteRenderer.sprite = deadSprite;

        // Trigger isDead animation
        animator.SetTrigger("isDead");

        // Wait for death
        yield return new WaitForSeconds(3);

        // Update score on GameManager
        gameManager.UpdateScore(pointValue);


        // Destroy gameObject
        Destroy(gameObject);
    }

}
