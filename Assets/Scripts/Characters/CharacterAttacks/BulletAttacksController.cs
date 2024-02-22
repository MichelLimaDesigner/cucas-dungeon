using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttacksController : MonoBehaviour
{
  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.CompareTag("Item") && !other.CompareTag("Player"))
    {
      Destroy(gameObject);
    }
  }
}
