using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerContorller", menuName = "PlayerSO", order = 0)]
public class PlayerControllerSO : ScriptableObject
{
  // -------------------- Attributes
  [Header("Player Attributes")]
  public PlayerScriptable currentCharacter;
  public List<PlayerScriptable> characters;
}
