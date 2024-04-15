using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform floor; // Assign the floor's transform here in the inspector
    private float spawnRate = 8.0f; // Time between spawns, in seconds
    private float lastSpawnTime = 0f; // Time since last spawn
    private int totalSpawns = 0; // Total number of enemies spawned
    private float maxSpawns = 30; // Maximum number of enemies to spawn
    private float spawnDuration = 120f; // Duration over which to reach max spawns, in seconds

    void Start()
    {
        // Start the spawning process
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (totalSpawns < maxSpawns)
        {
            if (Time.time - lastSpawnTime >= spawnRate)
            {
                SpawnEnemy();
                lastSpawnTime = Time.time;
                totalSpawns++;
            }
            yield return null;

            // Update spawn rate based on elapsed time to increase frequency
            spawnRate = Mathf.Lerp(2.0f, 0.1f, Time.time / spawnDuration);
        }
    }

    void SpawnEnemy()
    {
        // Get the size of the floor
        Vector3 floorSize = floor.localScale;

        // Calculate a random position within the floor bounds
        Vector3 spawnPosition = new Vector3(
            Random.Range(-floorSize.x / 2, floorSize.x / 2),
            2.00f,
            Random.Range(-floorSize.z / 2, floorSize.z / 2)
        ) + new Vector3(floor.position.x, floor.position.y, floor.position.z); ;

        // Instantiate the enemy at the calculated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
