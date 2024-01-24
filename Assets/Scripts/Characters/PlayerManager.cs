using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  [SerializeField] private Transform spawnPoint;
  [SerializeField] private PlayerControllerSO playerData;
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

  }

  public void SpawnCharacter()
  {
    if (!characterInstance)
    {
      characterInstance = Instantiate(playerData.currentCharacter.prefab, spawnPoint.position, Quaternion.identity);
    }
  }

  public void AddCharacter(PlayerScriptable newCharacter)
  {
    playerData.characters.Add(newCharacter);
  }

  public void SelectCharacter(int characterIndex)
  {
    if (characterInstance) Destroy(characterInstance);
    playerData.currentCharacter = playerData.characters[characterIndex];
    SpawnCharacter();
  }

  public void RemoveCharacter()
  {
    playerData.characters.Remove(playerData.currentCharacter);
    playerData.currentCharacter = default;
    Destroy(characterInstance);
    if (playerData.characters.Count > 0)
    {
      GameManager.Instance.UpdateGameState(GameState.SelectCharacter);
    }
    else
    {
      GameManager.Instance.UpdateGameState(GameState.Lose);
    }
  }
}
