using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public PlayerScriptable currentCharacter;
  public List<PlayerScriptable> characters;
  public Transform spawnPoint;

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
    Instantiate(currentCharacter.prefab, spawnPoint.position, Quaternion.identity);
  }

  public void SaveCharacter()
  {
    // Adicionar ao script do player para poder adicionar um novo personagem a lista
  }

  void ChangeCharacter()
  {
    // Seleciona um novo personagem
    if(Input.GetKeyDown(KeyCode.K))
    {
      var player = GameObject.FindGameObjectWithTag("Player");
      Destroy(player);
      currentCharacter = characters[0];
      SpawnCharacter();
    }
  }
}
