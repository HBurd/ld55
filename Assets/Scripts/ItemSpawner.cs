using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of item prefabs
    public Transform floor; // Assign the floor's transform here in the inspector
    public float spawnInterval = 5.0f; // Time between spawns, in seconds

    private Vector3 floorSize; // Size of the floor

    void Start()
    {
        floorSize = floor.localScale; // Get the size of the floor
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (true) // Loop to keep spawning items indefinitely
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        // Randomly select one of the item prefabs to spawn
        int itemIndex = Random.Range(0, itemPrefabs.Length);
        GameObject itemToSpawn = itemPrefabs[itemIndex];

        // Calculate a random position within the floor bounds
        Vector3 spawnPosition = new Vector3(
            Random.Range(-floorSize.x / 2, floorSize.x / 2),
            1.50f, // Adjust this value if you need a different height
            Random.Range(-floorSize.z / 2, floorSize.z / 2)
        ) + floor.position;

        // Instantiate the item at the calculated position
        Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
    }
}