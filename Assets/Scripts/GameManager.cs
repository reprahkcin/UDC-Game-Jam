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

    // Singleton
    public static GameManager instance;


    // Enemy Prefabs[]
    // TODO: Add enemies in the inspector
    public GameObject[] enemyPrefabs;

    // Item Prefabs[]
    // TODO: Add items in the inspector
    public GameObject[] itemPrefabs;

    // Fixed spawn points
    // TODO: Add spawn points in the inspector
    public Transform[] spawnPoints;

    public Transform itemSpawnPoint;

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

    // TODO: Add multiple waves
    // // Number of waves
    // [Range(1, 10)]
    // [Tooltip("Number of waves")]
    // public int waves = 5;


    // -------------------------------------------------
    // Lists
    // I am not using these currently, but I suppose it can't hurt to keep track of them
    // -------------------------------------------------

    // List of enemies
    public List<GameObject> enemies = new List<GameObject>();

    // List of items
    public List<GameObject> items = new List<GameObject>();

    // -------------------------------------------------
    // Variables
    // -------------------------------------------------

    // Current wave
    private int currentWave = 0;

    // Score
    public int score = 0;



    // -------------------------------------------------
    // Unity Functions
    // -------------------------------------------------
    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }

    public void RandomItemSpawn()
    {
        // Select a random item from the array
        int randomItem = Random.Range(0, itemPrefabs.Length);

        // Switch case for the random item
        switch (randomItem)
        {
            case 0:
                // Spawn Hotdog
                SpawnHotDog();
                break;
            case 1:
                // Spawn Pickle
                SpawnPickle();
                break;
            case 2:
                // Spawn Raw Dog
                SpawnRawDog();
                break;


            default:
                // Spawn Hotdog
                SpawnHotDog();
                break;
        }
    }


    private void Update()
    {
        // listen for right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            RandomItemSpawn();
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

        // Start theme music
        SoundManager.instance.PlayTheme1();

        // Start wave
        StartCoroutine(SpawnWave(spawnTimer));

    }


    // -------------------------------------------------
    // Death Sequence
    // TODO: Add death sequence
    // -------------------------------------------------

    // private bool death is over
    //private bool deathIsOver = false;
    // Death of Player
    public void DeathSwarm()
    {

        Debug.Log("Death Swarm!");
        // if (!deathIsOver)
        // {
        //     // spawn 50 enemies without delay
        //     for (int i = 0; i < 50; i++)
        //     {
        //         SpawnWave(0.01f);
        //     }
        // }
        // deathIsOver = true;

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


    IEnumerator HotDogTimer(float time)
    {
        yield return new WaitForSeconds(time);
        // spawn hot dog at item spawn point
        Instantiate(itemPrefabs[0], itemSpawnPoint.position, Quaternion.identity);

    }

    IEnumerator PickleTimer(float time)
    {
        yield return new WaitForSeconds(time);
        // spawn pickle at itemSpawnPoint
        Instantiate(itemPrefabs[1], itemSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator RawDogTimer(float time)
    {
        yield return new WaitForSeconds(time);
        // spawn raw dog at itemSpawnPoint
        Instantiate(itemPrefabs[2], itemSpawnPoint.position, Quaternion.identity);
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

    // Spawn Hot Dog
    public void SpawnHotDog()
    {
        // Player Script
        Player playerScript = Player.instance.GetComponent<Player>();

        // check player script for hot dog availability
        if (playerScript.GetHelpRequests() > 0)
        {

            // Trigger Emily Give_Item animation
            Emily.instance.GetComponent<Emily>().GiveItem();

            // wait until Emily is in the garage to spawn the hot dog
            StartCoroutine(HotDogTimer(2f));

            // decrement help requests
            playerScript.SetHelpRequests(playerScript.GetHelpRequests() - 1);


        }
    }

    // Spawn a Pickle
    public void SpawnPickle()
    {
        // Player Script
        Player playerScript = Player.instance.GetComponent<Player>();

        // check player script for hot dog availability
        if (playerScript.GetHelpRequests() > 0)
        {

            // Trigger Emily Give_Item animation
            Emily.instance.GetComponent<Emily>().GiveItem();

            // wait until Emily is in the garage to spawn the pickle
            StartCoroutine(PickleTimer(2f));

            // decrement help requests
            playerScript.SetHelpRequests(playerScript.GetHelpRequests() - 1);


        }
    }

    // Spawn Raw Dog
    public void SpawnRawDog()
    {
        // Player Script
        Player playerScript = Player.instance.GetComponent<Player>();

        // check player script for hot dog availability
        if (playerScript.GetHelpRequests() > 0)
        {

            // Trigger Emily Give_Item animation
            Emily.instance.GetComponent<Emily>().GiveItem();

            // wait until Emily is in the garage to spawn the raw dog
            StartCoroutine(RawDogTimer(2f));

            // decrement help requests
            playerScript.SetHelpRequests(playerScript.GetHelpRequests() - 1);


        }
    }





}
