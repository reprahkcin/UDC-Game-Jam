using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  // Item Timer
  [Range(0.1f, 10f)]
  [Tooltip("Time between item spawns")]
  public float itemTimer = 5f;

  // Number of Items per wave
  [Range(1, 10)]
  [Tooltip("Number of items to spawn in a wave")]
  public int itemsPerWave = 5;

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

  // Current enemy count
  private int currentEnemyCount = 0;

  // Current item count
  private int currentItemCount = 0;

  // Score
  private int score = 0;

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
      Destroy (gameObject);
    }
  }

  private void Start()
  {
    // Check if player exists
    if (GameObject.FindGameObjectWithTag("Player") == null)
    {
      // Instantiate player at the center of the screen
      Instantiate(player, Vector3.zero, Quaternion.identity);
    }
  }

  public void StartRound()
  {
    // If the current wave is less than the number of waves
    if (currentWave < waves)
    {
      // Spawn enemies
      StartCoroutine(SpawnWave());

      // Spawn Items
      StartCoroutine(SpawnItems());
    }
  }

  // -------------------------------------------------
  // Custom Functions
  // -------------------------------------------------
  // Spawns a wave of enemies
  IEnumerator SpawnWave()
  {
    // Spawn enemies
    for (int i = 0; i < enemiesPerWave; i++)
    {
      // Spawn enemy
      SpawnEnemy();

      // Wait for spawn timer
      yield return new WaitForSeconds(spawnTimer);
    }

    // Increment wave
    currentWave++;

    // Increment enemy count
    currentEnemyCount += enemiesPerWave;
  }

  IEnumerator SpawnItems()
  {
    // Spawn items
    for (int i = 0; i < itemsPerWave; i++)
    {
      // Spawn item
      SpawnItem();

      // Wait for spawn timer
      yield return new WaitForSeconds(spawnTimer);
    }

    // Increment item count
    currentItemCount += itemsPerWave;
  }

  // Coroutine for SpawnTimer
  IEnumerator SpawnTimer()
  {
    // Wait for spawn timer
    yield return new WaitForSeconds(spawnTimer);
  }

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
    enemies.Add (enemy);
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
    items.Add (item);
  }
}
