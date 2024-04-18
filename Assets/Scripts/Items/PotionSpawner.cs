using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotionSpawner : MonoBehaviour
{
  public GameObject[] potionsPrefabs;
  public Transform spawnPoint;

  public float spawnInterval = 3.0f;

  void Start()
  {
    StartCoroutine(SpawnPotions());
  }

  IEnumerator SpawnPotions()
  {
    yield return new WaitForSeconds(spawnInterval);
    SpawnPotion();
  }

  void SpawnPotion()
  {
    // Choose a random potion prefab
    GameObject randomPotionPrefab = potionsPrefabs[Random.Range(0, potionsPrefabs.Length)];

    // Instantiate the potion at the chosen spawn location
    GameObject newPotion = Instantiate(randomPotionPrefab, spawnPoint.position, Quaternion.identity);
    // Add more customizations here if needed
  }
}
