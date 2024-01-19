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

  }

  // Update is called once per frame
  void Update()
  {
    SelectCharacter(0);
  }

  void SpawnCharacter()
  {
    characterInstance = Instantiate(playerData.currentCharacter.prefab, spawnPoint.position, Quaternion.identity);
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
  }
}
