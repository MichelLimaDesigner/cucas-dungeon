using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadHit : MonoBehaviour
{
  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Enemy"))
    {
      other.GetComponent<Enemy>().TakeDamage();
      Player.Instance.MiniJump();
    }
  }
}
