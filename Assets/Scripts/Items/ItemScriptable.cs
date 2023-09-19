using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 2)]
public class ItemScriptable : ScriptableObject
{
  public string itemName;
  public string description;
  public Texture2D icon;
  public GameObject item;
}
