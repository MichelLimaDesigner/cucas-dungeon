using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureController : MonoBehaviour
{
  public PlayerScriptable character;

  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      PlayerManager.Instance.AddCharacter(character);
      Destroy(gameObject);
    }
  }
}
