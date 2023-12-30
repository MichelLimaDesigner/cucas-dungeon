using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "PotionsScriptable", order = 1)]
public class PotionsScriptable : ScriptableObject
{
  public string potionName;
  public string description;
  public Texture2D icon;
  public Texture2D sprite;
  public GameObject potion;
}
