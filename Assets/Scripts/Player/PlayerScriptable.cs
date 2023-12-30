using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerScriptable", order = 0)]
public class PlayerScriptable : ScriptableObject
{
  public PotionsScriptable potion;
  public List<PotionsScriptable> potionsInventory;
  public bool hasShield;
}
