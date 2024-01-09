using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerScriptable", order = 0)]
public class PlayerScriptable : ScriptableObject
{
  // -------------------- UI attributes
  public string characterName;
  public Texture2D avatar;
  public Texture2D card;

  // -------------------- Others
  public GameObject prefab;
  public PotionsScriptable potion;
  public List<PotionsScriptable> potionsInventory;
  public bool hasShield;
}
