using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour
{
  public GameObject[] enemyPrefabs; // Array contendo os prefabs dos inimigos
  public List<Transform> spawnLocations = new List<Transform>();

  public Transform spawnLocation; // Local onde os inimigos serão spawnados
  public int numberOfEnemiesToSpawn = 5; // Número total de inimigos a serem spawnados
  public float spawnInterval = 1.0f;
  void Start()
  {
    StartCoroutine(SpawnEnemies());
  }

  IEnumerator SpawnEnemies()
  {
    for (int i = 0; i < numberOfEnemiesToSpawn; i++)
    {
      SpawnEnemy();
      yield return new WaitForSeconds(spawnInterval);
    }
  }

  void Update()
  {
    if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
    {
      SpawnEnemy();
    }
  }

  void SpawnEnemy()
  {
    if (spawnLocations.Count == 0)
    {
      Debug.LogError("A lista de locais de spawner está vazia. Adicione pelo menos um local de spawner.");
      return;
    }

    // Escolhe aleatoriamente um local de spawner
    Transform randomSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];

    // Escolhe aleatoriamente um prefab de inimigo
    GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

    // Instancia o inimigo no local de spawner escolhido
    GameObject newEnemy = Instantiate(randomEnemyPrefab, randomSpawnLocation.position, Quaternion.identity);
    // Adicione mais personalizações aqui, se necessário
  }
}