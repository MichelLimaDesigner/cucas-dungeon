using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public Transform spawnPoint;
  public PlayerControllerSO playerData;
  private GameObject characterInstance;

  // Start is called before the first frame update
  void Start()
  {
    SpawnCharacter();
  }

  // Update is called once per frame
  void Update()
  {
    ChangeCharacter();
  }

  void SpawnCharacter()
  {
    characterInstance = Instantiate(playerData.currentCharacter.prefab, spawnPoint.position, Quaternion.identity);
  }

  public void AddCharacter(PlayerScriptable newCharacter)
  {
    playerData.characters.Add(newCharacter);
  }

  void ChangeCharacter()
  {
    // Seleciona um novo personagem
    if (Input.GetKeyDown(KeyCode.K))
    {
      var player = GameObject.FindGameObjectWithTag("Player");
      Destroy(player);
      playerData.currentCharacter = playerData.characters[0];
      SpawnCharacter();
    }
  }

  public void RemoveCharacter()
  {
    playerData.characters.Remove(playerData.currentCharacter);
    playerData.currentCharacter = default;
    Destroy(characterInstance);
  }
}
