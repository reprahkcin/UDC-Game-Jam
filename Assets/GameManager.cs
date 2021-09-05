using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Player Object
    public GameObject player;

    // Rat prefab
    public GameObject rat;

    // Rat spawn points
    public Transform[] spawnPoints;

    // Spawn timer
    public float spawnTimer = 5f;

    // List of rats in current scene
    public List<GameObject> rats;

    // Start with this many rats
    public int startRats = 5;

    public void SpawnRate()
    {
        // Get random spawn point
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Spawn rat
        GameObject ratClone =
            Instantiate(rat,
            spawnPoints[spawnPointIndex].position,
            Quaternion.identity);

        // Add to list
        rats.Add (ratClone);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Spawn initial rats
        for (int i = 0; i < startRats; i++)
        {
            SpawnRate();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
