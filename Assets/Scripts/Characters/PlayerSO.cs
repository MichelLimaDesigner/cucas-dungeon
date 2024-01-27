using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
  // -------------------- Attributes
  [Header("Player Attributes")]
  public PlayerTransformationSO transformation;
  // -------------------- UI attributes
  public string characterName;
  public Texture2D avatar;
  public Texture2D card;

  // -------------------- Others
  public GameObject prefab;
  public PotionsScriptable potion;
  public bool hasShield;
}
