using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerScriptable", order = 0)]
public class PlayerScriptable : ScriptableObject
{
  public int health = 8;
  public PotionsScriptable potionOne;
  public PotionsScriptable potionTwo;
  public ItemScriptable[] itemsInventory;
  public PotionsScriptable[] potionsInventory;
}
