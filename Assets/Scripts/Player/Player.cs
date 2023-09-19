using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  // Player properties
  public float speed;
  public float jumpForce;
  private float move;
  public PlayerScriptable attributes;

  // Actions controls
  private bool isJumping = false;
  private float direction;
  private bool isFacingRight = true;

  // Components
  private Rigidbody2D rig;

  // Start is called before the first frame update
  void Start()
  {
    rig = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    move = Input.GetAxis("Horizontal");
    // Moviment
    Move();
    Flip();
    Jump();
  }

  // -------------------- Player moviment
  void Move()
  {
    Vector3 movement = new Vector3(move, 0f, 0f);
    transform.position += movement * Time.deltaTime * speed;
  }

  void Flip()
  {
    if (move > 0 && !isFacingRight)
    {
      isFacingRight = !isFacingRight;
      transform.Rotate(0f, 180f, 0f);
    }
    else if (move < 0 && isFacingRight)
    {
      isFacingRight = !isFacingRight;
      transform.Rotate(0f, 180f, 0f);
    }
  }

  void Jump()
  {
    if (Input.GetButtonDown("Jump") && !isJumping)
    {
      rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
  }

  // -------------------- Damage and Die
  public void TakeDamage(int playerDamage)
  {
    attributes.health -= playerDamage;

    if (attributes.health <= 0)
    {
      Die();
    }
  }

  void Die()
  {
    Destroy(gameObject);
  }

  // -------------------- Collision functions
  private void OnCollisionEnter2D(Collision2D others)
  {
    if (others.gameObject.CompareTag("Enemy")) TakeDamage(1);
    if (others.gameObject.layer == 6) isJumping = false;
  }

  private void OnCollisionExit2D(Collision2D others)
  {
    if (others.gameObject.layer == 6) isJumping = true;
  }
}
