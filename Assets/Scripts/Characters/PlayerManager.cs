using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  [SerializeField] private Transform spawnPoint;
  [SerializeField] private PlayerSO playerData;
  [SerializeField] static private GameObject characterInstance;
  public static PlayerManager Instance;

  void Awake()
  {
    Instance = this;
    GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
  }

  void OnDestroy()
  {
    GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
  }

  private void GameManagerOnGameStateChanged(GameState state)
  {
    // Do something
  }

  // Start is called before the first frame update
  void Start()
  {
    Instantiate(playerData.prefab, spawnPoint.position, Quaternion.identity);
  }

  public void SpawnTransformation()
  {
    if (!characterInstance)
    {
      characterInstance = Instantiate(playerData.transformation.prefab, spawnPoint.position, Quaternion.identity);
    }
  }

  public void TransformPlayer(PlayerTransformationSO transformation)
  {
    playerData.transformation = transformation;
    SpawnTransformation();
  }

  public void LoseTransformation()
  {
    if (playerData.hasShield)
    {
      playerData.hasShield = false;
    }
    else if (!playerData.hasShield && playerData.transformation)
    {
      playerData.transformation = default;
      Destroy(characterInstance);
    }
    else
    {
      GameManager.Instance.UpdateGameState(GameState.GameOver);
    }
  }
}
