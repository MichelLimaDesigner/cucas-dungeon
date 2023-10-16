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
  public List<ItemScriptable> itemsList;
  private bool canShoot = true;

  // Actions controls
  private bool isJumping = false;
  private float direction;
  private bool isFacingRight = true;

  // Components and Objects
  private Rigidbody2D rig;
  public Transform firePoint;
  public GameObject bulletPrefab;


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
    Shoot();
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
    if (others.gameObject.CompareTag("Enemy")) TakeDamage(1);
    if (others.gameObject.layer == 6) isJumping = false;
  }

  private void OnCollisionExit2D(Collision2D others)
  {
    if (others.gameObject.layer == 6) isJumping = true;
  }

  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Enemy"))
    {
      TakeDamage(1);
    }
    else if (other.CompareTag("Item"))
    {
      AddItemToInventory(other.gameObject.name);
    }
  }
}
