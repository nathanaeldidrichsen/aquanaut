using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public GameObject spawnLocation; // The location where the prefab will be spawned
    public float spawnInterval = 5.0f; // The time interval between spawns

    private float timer; // A timer to track the time between spawns

    private void Start()
    {
        // Initialize the timer
        timer = spawnInterval;
    }

    private void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // Check if the timer has reached zero
        if (timer <= 0f)
        {
            // Spawn the prefab at the specified location
            Instantiate(prefabToSpawn, spawnLocation.transform.position, Quaternion.identity);

            // Reset the timer
            timer = spawnInterval;
        }
    }
}
