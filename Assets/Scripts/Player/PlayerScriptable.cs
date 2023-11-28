using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerScriptable", order = 0)]
public class PlayerScriptable : ScriptableObject
{
  public int maxItems = 12;
  public PotionsScriptable potionOne;
  public PotionsScriptable potionTwo;
  public List<ItemScriptable> itemsInventory;
  public List<PotionsScriptable> potionsInventory;
}
