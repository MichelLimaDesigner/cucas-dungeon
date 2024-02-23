using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public static Enemy Instance;

  void Awake()
  {
    Instance = this;
  }

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

  // -------------------- Collision functions
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      if (!PlayerManager.Instance.isIntangible) PlayerManager.Instance.TakeDamage();
    }
  }

  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Attack"))
    {
      TakeDamage();
    }
  }
}
