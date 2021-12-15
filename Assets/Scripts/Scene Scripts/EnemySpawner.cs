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
    private bool endlessSpawn = false;      // To check whether there should be a maximum amount of enemies spawned
    
    [SerializeField]
    private int totalSpawnAmount; // How many enemies will spawn from this spawner stored via Unity Editor

    [SerializeField]
    private int enemiesPerSpawn; // How many enemies will spawn during each spawning instance

    [SerializeField]
    private bool monsterSpawningMonster = false;      // To check if the spawner is actually a monster which spawns other monsters

    [SerializeField]
    private List<Transform> spawnZones = new List<Transform>(); // Dedicated spawn zone locations stored in the list via Unity Editor

    [SerializeField]
    private List<GameObject> enemyPrefabs = new List<GameObject>(); // Allowed enemy prefabs will be stored in the list via Unity Editor

    [SerializeField]
    private bool sequentialSpawnZones = false;      // Sets the spawner to choose spawn locations in order

    [SerializeField]
    private bool sequentialEnemySpawns = false;      // Sets the spawner to choose spawned enemies in order

    private bool isAwake = false;   // If the spawner is 'activated.' Player touching the collider activates it

    private int totalEnemiesSpawned = 0; // How many enemies have been spawned from this spawner

    private int enemyListSize;  // Stores the size of the enemy list to use as a bound later.
    private int spawnListSize;  // Stores the size of the spawn list to use as a bound later.

    private int chosenSpawn = 0; // This chooses a random number to pick from the spawn zones
    private int chosenEnemy = 0; // This chooses a random number to pick from the enemies array

    private GameObject enemy; // This grabs the enemy's game object from the list and stores it
    private Transform enemySpawn; // This stores the transform of the chosen spawn zone

    [SerializeField]
    private int spawnDelay = 3; // Amount of time between each enemy spawn 
    private float timer = 0.0f; // Holds how many seconds have passed
    private bool isSpawning = false; // Records when an enemy is spawning so we dont have multiple spawning each frame one is supposed to spawn.

    private void OnCollisionEnter2D(Collision2D col)  // if they hit something
    {
        if (col.gameObject.tag == "Player" && !monsterSpawningMonster)      // if its a player and the spawner is not a monster 
        {
            isAwake = true;         // activates the spawner to start spawning
            gameObject.GetComponent<BoxCollider2D>().enabled = false;   // turns off the physical spawner collider
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        enemyListSize = enemyPrefabs.Count;
        spawnListSize = spawnZones.Count;

        if (monsterSpawningMonster)
            isAwake = true;             // monsters will start already spawning monsters
        else
            timer = (float)spawnDelay;     //  makes it so the spawner will spawn an enemy immediately upon awaking
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAwake)
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
        if ((totalEnemiesSpawned < totalSpawnAmount) || endlessSpawn)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                ChooseSpawnZone();
                ChooseSpawnedEnemy();

                Instantiate(enemyPrefabs[chosenEnemy], spawnZones[chosenSpawn].transform.position, transform.rotation);     // creates an enemy of the chose type at the chose spawn zone's location

                totalEnemiesSpawned++;
            }
        }
    }

    private void ChooseSpawnZone()
    {
        if (sequentialSpawnZones)
        {
            if (chosenSpawn < spawnListSize - 1)
                chosenSpawn++;
            else
                chosenSpawn = 0;
        }
        else
            chosenSpawn = Random.Range(0, spawnListSize);   // grabs a random index of the list
    }

    private void ChooseSpawnedEnemy()
    {
        if (sequentialEnemySpawns)
        {
            if (chosenEnemy < enemyListSize - 1)
                chosenEnemy++;
            else
                chosenEnemy = 0;
        }
        else
            chosenEnemy = Random.Range(0, enemyListSize);   // grabs a random index of the list
    }
}
