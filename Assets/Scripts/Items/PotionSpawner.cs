using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotionSpawner : MonoBehaviour
{
    public GameObject[] potionsPrefabs;
    public List<Transform> spawnLocations = new List<Transform>();

    public int numberOfPotionsToSpawn = 5;
    public float spawnInterval = 1.0f;

    void Start()
    {
        StartCoroutine(SpawnPotions());
    }

    IEnumerator SpawnPotions()
    {
        for (int i = 0; i < numberOfPotionsToSpawn; i++)
        {
            if (spawnLocations.Count > 0)
            {
                // Get a random spawn location that hasn't been used yet
                Transform randomSpawnLocation = GetUnusedSpawnLocation();

                if (randomSpawnLocation != null)
                {
                    SpawnPotion(randomSpawnLocation);
                }
                else
                {
                    Debug.LogError("All spawn locations have been used.");
                    yield break;
                }
            }
            else
            {
                Debug.LogError("The list of spawn locations is empty. Add at least one spawn location.");
                yield break;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Transform GetUnusedSpawnLocation()
    {
        List<Transform> unusedLocations = new List<Transform>();

        foreach (Transform location in spawnLocations)
        {
            // Check if the location has no potion spawned at it
            if (!LocationHasPotion(location.position))
            {
                unusedLocations.Add(location);
            }
        }

        // Return a random unused location, or null if all locations have been used
        return unusedLocations.Count > 0 ? unusedLocations[Random.Range(0, unusedLocations.Count)] : null;
    }

    bool LocationHasPotion(Vector3 position)
    {
        // Check if a potion already exists at the given position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f); // Adjust the radius as needed

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                return true;
            }
        }

        return false;
    }

    void SpawnPotion(Transform spawnLocation)
    {
        // Choose a random potion prefab
        GameObject randomPotionPrefab = potionsPrefabs[Random.Range(0, potionsPrefabs.Length)];

        // Instantiate the potion at the chosen spawn location
        GameObject newPotion = Instantiate(randomPotionPrefab, spawnLocation.position, Quaternion.identity);
        // Add more customizations here if needed
    }
}
