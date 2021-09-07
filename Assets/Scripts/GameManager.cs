using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // -------------------------------------------------
    // Required GameObjects
    // -------------------------------------------------
    public static GameManager gm;

    // Player Object
    // TODO: Add Player prefab in the inspector
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

    // Bool to trigger item spawn
    private bool spawnItem = false;

    // Hot Dogs available
    private int hotDogs = 4;

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
    }


    private void Update()
    {
        // listen for key h
        // if Player clicks right mouse button, set spawnItem to true
        // if (Input.GetKeyDown(1))
        if (Input.GetKeyDown(KeyCode.H))
        {
            // if hotDogs > 0, spawn item
            if (hotDogs > 0)
            {
                spawnItem = true;
                hotDogs--;
            }
        }

        // If spawnItem is true, spawn an item
        if (spawnItem)
        {
            SpawnItem();
            spawnItem = false;
        }

    }

    // -------------------------------------------------
    // Gameplay Functions
    // -------------------------------------------------
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

        // spawn items
        SpawnItem();

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
    void SpawnEnemy()
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
    void SpawnItem()
    {
        // Get random spawn point
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Get random item
        int itemIndex = Random.Range(0, itemPrefabs.Length);

        // Instantiate item
        GameObject item =
          Instantiate(itemPrefabs[itemIndex],
          spawnPoints[spawnPointIndex].position,
          spawnPoints[spawnPointIndex].rotation);

        // Add item to list
        items.Add(item);
    }
}
