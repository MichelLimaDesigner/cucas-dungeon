using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  // -------------------- Player attributes
  [Header("Player Attributes")]
  public float speed;
  public float jumpForce;
  private float move;
  public PlayerScriptable attributes;
  public List<ItemScriptable> itemsList;
  private bool canShoot = true;

  // -------------------- Actions controls
  private bool isJumping = false;
  private float direction;
  private bool isGrounded;
  public bool isFacingRight = true;
  private bool jumpInputReleased;
  private bool active = true;
  private Vector2 respawnPoint;

  // -------------------- Components and Objects
  private Rigidbody2D rig;
  private Collider2D playerCollider;
  public Transform firePoint;
  public GameObject bulletPrefab;
  public TrailRenderer trailRenderer;

  // -------------------- Dashing properties
  [Header("Dashing")]
  public float dashingVelocity = 5f;
  public float dashingTime = 0.1f;
  private Vector2 dashingDir;
  private bool isDashing = false;
  private bool canDash = true;

  // -------------------- Start is called before the first frame update
  void Start()
  {
    rig = GetComponent<Rigidbody2D>();
    playerCollider = GetComponent<Collider2D>();
    trailRenderer = GetComponent<TrailRenderer>();
    SetRespawnPoint(transform.position);
  }

  // -------------------- Update is called once per frame
  void Update()
  {
    // -------------------- Variables
    move = Input.GetAxis("Horizontal");

    jumpInputReleased = Input.GetButtonUp("Jump");


    // -------------------- Methods
    Move();
    Flip();
    Jump();
    Shoot();
    Dash();

    // -------------------- Return if player die's
    if (!active)
    {
      return;
    }
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
    Vector2 velocity = rig.velocity;

    if (Input.GetButtonDown("Jump") && !isJumping)
    {
      velocity.y = jumpForce;
      rig.velocity = velocity;
      isJumping = true;
    }

    if (jumpInputReleased && rig.velocity.y > 0)
    {
      velocity.y = 0;
      rig.velocity = velocity;
    }
  }

  // -------------------- Dashing

  private void Dash()
  {
    if (Input.GetButtonDown("Dash") && canDash)
    {
      isDashing = true;
      canDash = false;
      trailRenderer.emitting = true;
      float dir;

      // Quando o jogador está parado
      if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
      {
        dir = isFacingRight ? 1f : -1f;
      }
      // Quando o jogador usa o dash para cima ou para baixo
      else if (Input.GetAxisRaw("Horizontal") == 0)
      {
        dir = 0f;
      }
      // Quando o jogador está apontando uma direção
      else
      {
        dir = move;
      }

      dashingDir = new Vector2(dir, Input.GetAxisRaw("Vertical"));

      if (dashingDir == Vector2.zero)
      {
        dashingDir = new Vector2(transform.localScale.x, 0);
      }

      StartCoroutine(StopDashing());
    }

    if (isDashing)
    {
      rig.velocity = dashingDir.normalized * dashingVelocity;
      return;
    }

    // ------------------ Implementar 
    // if(isGrounded)
    // {
    //   canDash = true;
    // }
  }


  IEnumerator StopDashing()
  {
    yield return new WaitForSeconds(dashingTime);
    trailRenderer.emitting = false;
    isDashing = false;
    var velocity = rig.velocity;
    velocity.x = 0;
    rig.velocity = velocity;
    if (isGrounded)
    {
      canDash = true;
    }
  }

  // -------------------- Die and Respawn
  public void SetRespawnPoint(Vector2 position)
  {
    respawnPoint = position;
  }

  private IEnumerator Respawn()
  {
    yield return new WaitForSeconds(1f);
    transform.position = respawnPoint;
    active = true;
    playerCollider.enabled = true;
    DieMiniJump();
  }

  public void TakeDamage()
  {
    bool hasShield = false;

    if (!hasShield)
    {
      Die();
    }
  }

  private void DieMiniJump()
  {
    Vector2 velocity = rig.velocity;
    velocity.y = jumpForce / 2;
    velocity.x = 0;
    rig.velocity = velocity;
  }

  public void Die()
  {
    active = false;
    playerCollider.enabled = false;
    DieMiniJump();
    StartCoroutine(Respawn());
  }

  // -------------------- Attack and Items functions

  void Shoot()
  {
    if (Input.GetButtonDown("Fire1") && canShoot)
    {
      canShoot = false;
      Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
      StartCoroutine(SetCanShoot(1));
    }
  }

  IEnumerator SetCanShoot(int time)
  {
    yield return new WaitForSeconds(time);

    canShoot = true;
  }

  // -------------------- Invetory functions
  public ItemScriptable FindItemByName(string itemName)
  {
    ItemScriptable itemFound = itemsList.Find(item => item.itemName == itemName);
    return itemFound;
  }

  public void AddItemToInventory(string itemName)
  {
    ItemScriptable itemFound = FindItemByName(itemName);

    if (itemFound != null)
    {
      attributes.itemsInventory.Add(itemFound);

      Debug.Log("Item adicionado ao inventário: " + itemFound.itemName);
    }
    else
    {
      Debug.Log("Item não encontrado para adicionar ao inventário.");
    }
  }

  private void AddNewPotion()
  {
    Debug.Log("New potion added");
  }


  // -------------------- Collision functions
  private void OnCollisionEnter2D(Collision2D others)
  {
    if (others.gameObject.CompareTag("Enemy")) TakeDamage();
    // Player is on ground
    if (others.gameObject.layer == 6)
    {
      isJumping = false;
      isGrounded = true;
      if (!isDashing) canDash = true;
    }
  }

  private void OnCollisionExit2D(Collision2D others)
  {
    if (others.gameObject.layer == 6)
    {
      isGrounded = false;
    }
  }

  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Death"))
    {
      TakeDamage();
    }
    else if (other.CompareTag("Item"))
    {
      AddItemToInventory(other.gameObject.name);
    }
  }
}
