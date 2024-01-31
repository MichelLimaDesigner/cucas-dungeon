using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPotionAttack : MonoBehaviour
{
  public float speed = 0f;
  public int damage = 1;

  // Start is called before the first frame update
  void Start()
  {
    var direction = transform.right + Vector3.up;
    GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += transform.right * speed * Time.deltaTime;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    Enemy enemy = other.GetComponent<Enemy>();
    if (enemy) enemy.TakeDamage();
    else if (other.CompareTag("Player")) return;
    else Destroy(gameObject);
  }
}
