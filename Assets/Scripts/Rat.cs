using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------

    // Rigidbody2D
    public Rigidbody2D rb;

    // Rat GameObject
    public GameObject rat;

    // ShovelHolder GameObject
    public GameObject shovelHolder;

    // Rat's Animator
    public Animator anim;

    // Rat's Sprite Renderer
    public SpriteRenderer spriteRenderer;

    // Dead Sprite
    public Sprite deadSprite;

    // Health Bar Canvas
    public RectTransform healthBarCanvas;

    // Health Bar GameObject
    public GameObject healthBar;

    // Speed of the Rat
    private float speed = 1.0f;

    // Max Health of the Rat
    private float maxHealth = 50.0f;

    // Health of the Rat
    public float health = 50.0f;

    // Rat Attack Damage
    private float attackDamage = 1f;

    // Point value of the Rat
    private int pointValue = 10;

    // Boolean to check if the Rat is dead
    private bool isAlive = true;

    // Boolean to check if Rat is being hit
    private bool isHit = false;



    // ------------------------------------------------------------------------
    // Method for rotating the enemy, and also counter-rotating 
    // health bar to stay upright.
    // ------------------------------------------------------------------------

    private void FacePlayer()
    {
        Vector3 direction = Player.instance.GetComponent<Transform>().position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;

        // Get Enemy local rotation
        Vector3 localRotation = transform.localEulerAngles;

        //
        // Rotate the healthBarCanvas to always stay upright
        healthBarCanvas.localEulerAngles = new Vector3(0, 0, -(localRotation.z));
    }

    public void PursuePlayer()
    {
        // Move Enemy forward at speed
        rb.velocity = transform.up * speed;
    }

    public void ScurryAway()
    {

        // Turn around 180 degrees
        rb.rotation += 180;

        // Move away from player at speed * 2
        rb.velocity = transform.up * speed * -2;
    }

    public void KnockBack()
    {
        rb.velocity = transform.up * speed * -40;
    }

    // ------------------------------------------------------------------------
    // Health Bar
    // ------------------------------------------------------------------------

    // Update Health Bar
    public void UpdateHealthBar()
    {
        // Get the RectTransform of the healthBar
        RectTransform healthBarRect = healthBar.GetComponent<RectTransform>();

        // Get the ratio of health to max health
        float ratio = health / maxHealth;

        // Set HealthBarObject's local scale x to health
        healthBarRect.localScale = new Vector3(ratio, 1, 1);
    }

    // ------------------------------------------------------------------------
    // Various Methods
    // ------------------------------------------------------------------------

    // Take Damage From player or poison, or whatever
    public void TakeDamage(float damage)
    {
        // Subtract damage from health
        health -= damage;

        // RatHit Animation
        anim.SetBool("isHit", true);

        // wait for 0.1 seconds
        StartCoroutine(WaitForAnimation());

    }

    // Wait for the RatHit Animation to finish
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.1f);

        // Set RatHit Animation to false
        anim.SetBool("isHit", false);
    }

    // Damage Player
    public void DamagePlayer()
    {
        // convert attack damage to int
        int damage = (int)attackDamage;

        Player.instance.DamagePlayer(damage);
    }

    // ------------------------------------------------------------------------
    // Unity Methods
    // ------------------------------------------------------------------------

    void Update()
    {
        if (isAlive && !isHit)
        {
            FacePlayer();
            PursuePlayer();
            UpdateHealthBar();
        }
        else if (isAlive && isHit)
        {
            KnockBack();
        }
        // If health is less than or equal to 0, kill the rat
        if (health <= 0)
        {
            // Set isAlive to false
            isAlive = false;
            // Death Sequence
            StartCoroutine(DeathTimer(3f));
        }
    }

    // On Trigger Enter
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the other object is the player and the player is poisoned
        if (other.gameObject.tag == "Consumer" && Player.instance.isPoisoned)
        {
            Debug.Log("Player Poison damage: " + Player.instance.poisonDamage);
            // take poison damage
            TakeDamage(Player.instance.poisonDamage);

        }
        // If the other object is the player
        if (other.gameObject.tag == "Consumer" && !Player.instance.isPoisoned)
        {
            int attackDamageInt = (int)attackDamage;
            Player.instance.DamagePlayer(attackDamageInt);
            Debug.Log("Player Hit");
        }


        // If the other object is the player's shovel
        if (other.gameObject.tag == "Shovel")
        {
            // Set isHit to true
            isHit = true;

            // Take Damage
            TakeDamage(Player.instance.attackDamage);

            // Hit Timer
            StartCoroutine(HitTimer(0.05f));
        }

    }


    // ------------------------------------------------------------------------
    // Timers
    // ------------------------------------------------------------------------

    // Timer for death
    IEnumerator DeathTimer(float time)
    {
        // stop movement
        //rb.velocity = Vector2.zero;
        rb.simulated = false;

        // Change the sprite to dead
        spriteRenderer.sprite = deadSprite;

        // Trigger isDead animation
        anim.SetTrigger("isDead");

        // Wait for death
        yield return new WaitForSeconds(time);

        // Update score on GameManager
        GameManager.instance.GetComponent<GameManager>().UpdateScore(pointValue);

        // Destroy gameObject
        Destroy(gameObject);
    }

    // Timer for hit
    IEnumerator HitTimer(float time)
    {
        // Wait for hit
        yield return new WaitForSeconds(time);

        // Set isHit to false
        isHit = false;
    }


}
