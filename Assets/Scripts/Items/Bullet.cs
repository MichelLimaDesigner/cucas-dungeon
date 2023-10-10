using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  // Bullet properties
  public float speed = 3f;
  public int damage = 1;
  public Rigidbody2D rig;
  // Start is called before the first frame update
  void Start()
  {
    rig.velocity = transform.right * speed;
    StartCoroutine(DestroyBullet(1.5f));
  }

  IEnumerator DestroyBullet(float time)
  {
    yield return new WaitForSeconds(time);

    Destroy(gameObject);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    Enemy enemy = other.GetComponent<Enemy>();

    if (enemy != null)
    {
      enemy.TakeDamage(damage);
    }

    Destroy(gameObject);
  }
}