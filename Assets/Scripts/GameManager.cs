using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    // -------------------------------------------------
    // Required GameObjects
    // -------------------------------------------------
    public static GameManager gm;

    // Emily
    private GameObject emily;

    // Player Object
    public GameObject player;

    // Enemy Prefabs[]
    // TODO: Add enemies in the inspector
    public GameObject[] enemyPrefabs;

    // Item Prefabs[]
    // TODO: Add items in the inspector
    public GameObject[] itemPrefabs;

    // Fixed spawn points
    // TODO: Add spawn points in the inspector
    public Transform[] spawnPoints;

    // -------------------------------------------------
    // Gameplay Variables
    // -------------------------------------------------
    // Spawn timer
    [Range(0.1f, 10f)]
    [Tooltip("Time between enemy spawns")]
    public float spawnTimer = 5f;

    // Number of enemies to spawn in a wave
    [Range(1, 100)]
    [Tooltip("Number of enemies to spawn in a wave")]
    public int enemiesPerWave = 5;

    // Number of waves
    [Range(1, 10)]
    [Tooltip("Number of waves")]
    public int waves = 5;

    // -------------------------------------------------
    // Lists
    // -------------------------------------------------
    // List of enemies
    public List<GameObject> enemies = new List<GameObject>();

    // List of items
    public List<GameObject> items = new List<GameObject>();

    // -------------------------------------------------
    // Private Variables
    // -------------------------------------------------
    // Current wave
    private int currentWave = 0;

    // Score
    // TODO: Add score incrementer to Enemy Script
    public int score = 0;



    // -------------------------------------------------
    // Unity Functions
    // -------------------------------------------------
    private void Awake()
    {
        // Singleton
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }

        // Set Emily
        emily = GameObject.Find("Emily");

    }


    private void Update()
    {
        // listen for right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            SpawnHotDog();
        }
    }




    // -------------------------------------------------
    // Gameplay Functions
    // -------------------------------------------------

    // Spawn Hot Dog
    public void SpawnHotDog()
    {
        // Player Script
        Player playerScript = player.GetComponent<Player>();

        // check player script for hot dog availability
        if (playerScript.GetPowerups() > 0)
        {

            // Trigger Emily Give_Item animation
            emily.GetComponent<Emily>().GiveItem();

            // spawn hot dog at spawnPoint[1]
            Instantiate(itemPrefabs[0], spawnPoints[1].position, Quaternion.identity);

            // decrement hot dogs
            playerScript.SetPowerups(playerScript.GetPowerups() - 1);


        }
    }

    // Update Score
    public void UpdateScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return score;
    }


    public void StartGame()
    {
        // Spawn Player
        // Check if player exists
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            // Instantiate player at the center of the screen
            Instantiate(player, Vector3.zero, Quaternion.identity);
        }

        // Start wave
        StartCoroutine(SpawnWave(spawnTimer));

    }

    // private bool death is over
    private bool deathIsOver = false;
    // Death of Player
    public void PlayerDeath()
    {
        if (!deathIsOver)
        {

            // spawn 50 enemies without delay
            for (int i = 0; i < 50; i++)
            {
                SpawnWave(0.01f);
            }
        }
        deathIsOver = true;

    }
    // -------------------------------------------------
    // Timer Functions
    // -------------------------------------------------

    // Spawn wave of enemies
    IEnumerator SpawnWave(float timeDelay)
    {

        // Spawn enemies
        for (int i = 0; i < enemiesPerWave; i++)
        {
            // Get random enemy
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);

            // Get random spawn point
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            // Instantiate enemy
            GameObject enemy = Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);

            // Add enemy to list
            enemies.Add(enemy);
            // Wait for spawn timer
            yield return new WaitForSeconds(timeDelay);
        }


        // Increment wave
        currentWave++;
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }


    // -------------------------------------------------
    // Spawn Functions
    // -------------------------------------------------

    // Spawns an enemy
    public void SpawnEnemy()
    {
        // Get random spawn point
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Get random enemy
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // Instantiate enemy
        GameObject enemy =
          Instantiate(enemyPrefabs[enemyIndex],
          spawnPoints[spawnPointIndex].position,
          spawnPoints[spawnPointIndex].rotation);

        // Add enemy to list
        enemies.Add(enemy);
    }

    // Spawns an item
    public void SpawnItem()
    {
        // Tell Emily to give an item
        emily.GetComponent<Emily>().GiveItem();

        // Wait for Emily to enter the garage
        StartCoroutine(ItemTimer(2f));
    }

    // -------------------------------------------------
    // Timer
    // -------------------------------------------------

    IEnumerator ItemTimer(float time)
    {
        yield return new WaitForSeconds(time);
        // Spawn item
        // instantiate HotDog at spawnPoint[1]
        Instantiate(itemPrefabs[0], spawnPoints[1].position, Quaternion.identity);

    }
}
