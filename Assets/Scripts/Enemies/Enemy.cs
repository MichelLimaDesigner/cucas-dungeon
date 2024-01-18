using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public PlayerController controller;

  public void TakeDamage()
  {
    var hasShield = false;

    if (hasShield)
    {
      hasShield = false;
    }
    else
    {
      Die();
    }
  }

  void Die()
  {
    Destroy(gameObject);
  }

  // -------------------- Trigger functions
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      controller.RemoveCharacter();
    }
  }
}
