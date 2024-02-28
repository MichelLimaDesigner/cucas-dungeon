using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotionSpawner : MonoBehaviour
{
  public GameObject[] potionsPrefabs;
  // public List<Transform> spawnLocations = new List<Transform>();
  public Transform spawnPoint;

  public float spawnInterval = 3.0f;

  void Start()
  {
    StartCoroutine(SpawnPotions());
  }

  IEnumerator SpawnPotions()
  {
    if (!LocationHasPotion())
    {
      yield return new WaitForSeconds(spawnInterval);
      SpawnPotion();
    }
  }

  bool LocationHasPotion()
  {
    // Check if a potion already exists at the given position
    Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, 0.5f); // Adjust the radius as needed

    foreach (Collider2D collider in colliders)
    {
      if (collider.CompareTag("Item"))
      {
        return true;
      }
    }

    return false;
  }

  void SpawnPotion()
  {
    // Choose a random potion prefab
    GameObject randomPotionPrefab = potionsPrefabs[Random.Range(0, potionsPrefabs.Length)];

    // Instantiate the potion at the chosen spawn location
    GameObject newPotion = Instantiate(randomPotionPrefab, spawnPoint.position, Quaternion.identity);
    // Add more customizations here if needed
  }

  void OnTriggerEnter2D(Collider2D others)
  {
    if (others.CompareTag("Player"))
    {
      if (!LocationHasPotion())
      {
        StartCoroutine(SpawnPotions());
      }
    }
  }
}
