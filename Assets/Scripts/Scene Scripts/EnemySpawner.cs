// Elijah Nieves

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds the responsibility of spawning a predetermined amount of enemies,
/// from a predetermined list of enemys, at predetermined locations, at a predetermined rate.
/// This class will most likely never be inherited from so everything is private.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int spawnAmount; // How many enemies will spawn from this spawner stored via Unity Editor

    [SerializeField]
    private List<Transform> spawnZones = new List<Transform>(); // Dedicated spawn zone locations stored in the list via Unity Editor

    [SerializeField]
    private List<GameObject> enemyPrefabs = new List<GameObject>(); // Allowed enemy prefabs will be stored in the list via Unity Editor

    private int enemiesSpawned = 0; // How many enemies have been spawned from this spawner

    private int enemyListSize;  // Stores the size of the enemy list to use as a bound later.
    private int spawnListSize;  // Stores the size of the spawn list to use as a bound later.

    private int chosenSpawn; // This chooses a random number to pick from the spawn zones
    private int chosenEnemy; // This chooses a random number to pick from the enemies array

    private GameObject enemy; // This grabs the enemy's game object from the list and stores it
    private Transform enemySpawn; // This stores the transform of the chosen spawn zone

    [SerializeField]
    private int spawnDelay = 3; // Amount of time between each enemy spawn 
    private float timer = 0.0f; // Holds how many seconds have passed
    private bool isSpawning = false; // Records when an enemy is spawning so we dont have multiple spawning each frame one is supposed to spawn.

    // Start is called before the first frame update
    private void Start()
    {
        enemyListSize = enemyPrefabs.Count;
        spawnListSize = spawnZones.Count;
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;        // each frame, add how much time has passed.
        
        if (!isSpawning)
        {
            if (timer > spawnDelay)  // if we should not be currently delaying,
            {
                timer = 0.0f;   // reset timer
                isSpawning = true;
            }
        }
    }
    
    // FixedUpdate is called at a fixed interval, not always once per frame.
    private void FixedUpdate()
    {
        if (isSpawning)
        {
            SpawnEnemy();
            isSpawning = false;
        }
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned < spawnAmount)
        {
            chosenSpawn = Random.Range(0, spawnListSize);   // grabs a random index of the list
            chosenEnemy = Random.Range(0, enemyListSize);

            Instantiate(enemyPrefabs[chosenEnemy], spawnZones[chosenSpawn].transform.position, transform.rotation);     // creates an enemy of the chose type at the chose spawn zone's location

            enemiesSpawned++;
        }
    }
}
