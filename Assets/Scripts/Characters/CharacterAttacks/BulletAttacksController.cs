using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttacksController : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    Enemy enemy = other.GetComponent<Enemy>();

    if (enemy)
    {
      enemy.TakeDamage();
      Destroy(gameObject);
    }
  }
}
