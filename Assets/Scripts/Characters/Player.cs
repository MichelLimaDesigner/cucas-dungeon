using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public static Player Instance;
  // -------------------- Player attributes
  [Header("Player Attributes")]
  private float move;
  public PlayerSO attributes;

  // -------------------- Actions controls
  public bool isFacingRight = true;
  private bool jump;
  private bool active = true;

  // -------------------- Components and Objects
  private Rigidbody2D rig;
  private Collider2D playerCollider;
  public GameObject bulletPrefab;
  public Animator animator;
  public GameObject transformationAnim;
  AudioManager audioManager;

  // -------------------- Ground & wall system
  [Header("Ground system")]
  private bool isGrounded;
  public Transform groundCheck;
  public LayerMask groundLayer;

  void Awake()
  {
    Instance = this;
    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
  }

  // -------------------- Start is called before the first frame update
  void Start()
  {
    rig = GetComponent<Rigidbody2D>();
    playerCollider = GetComponent<Collider2D>();
  }

  // -------------------- Update is called once per frame
  void Update()
  {
    // -------------------- Variables
    move = Input.GetAxis("Horizontal");
    jump = Input.GetButtonUp("Jump");
    isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.2f, 0.1f), 0, groundLayer);

    // -------------------- Methods
    Flip();
    Jump();

    // -------------------- Return if player die's
    if (!active)
    {
      return;
    }
  }

  private void FixedUpdate()
  {
    Move();
  }

  // -------------------- Player moviment
  void Move()
  {
    Vector3 movement = new Vector3(move, 0f, 0f);
    transform.position += movement * Time.deltaTime * attributes.speed;
    animator.SetFloat("Speed", Mathf.Abs(move)); // Set run animation
    // audioManager.PlaySFX(audioManager.walkSFX);
  }

  void Flip()
  {
    if (move > 0 && !isFacingRight)
    {
      // Flip to Left
      isFacingRight = !isFacingRight;
      transform.Rotate(0f, 180f, 0f);
    }
    else if (move < 0 && isFacingRight)
    {
      // Flip to Right
      isFacingRight = !isFacingRight;
      transform.Rotate(0f, 180f, 0f);
    }
  }

  void Jump()
  {
    Vector2 velocity = rig.velocity;

    animator.SetBool("isJumping", !isGrounded);

    if (Input.GetButtonDown("Jump"))
    {
      if (isGrounded)
      {
        velocity.y = attributes.jumpForce;
        rig.velocity = velocity;
      }
      else if (!isGrounded && attributes.canFly)
      {
        // Se estiver no ar, aplica uma força para simular o voo.
        rig.velocity = new Vector2(rig.velocity.x, attributes.jumpForce / 2);
        rig.AddForce(Vector2.up * attributes.flightForce, ForceMode2D.Impulse);
      }
    }

    if (jump && rig.velocity.y > 0)
    {
      velocity.y = 0;
      rig.velocity = velocity;
    }
  }

  // -------------------- Die

  public void MiniJump()
  {
    Vector2 velocity = rig.velocity;
    velocity.y = attributes.jumpForce / 2;
    velocity.x = 0;
    rig.velocity = velocity;
  }

  // -------------------- Collision functions
  private void OnCollisionEnter2D(Collision2D others)
  {
    if (others.gameObject.CompareTag("Enemy"))
    {
      if (!PlayerManager.Instance.isIntangible) PlayerManager.Instance.TakeDamage();
    }
    if (others.gameObject.CompareTag("Damage"))
    {
      if (!PlayerManager.Instance.isIntangible)
      {
        PlayerManager.Instance.TakeDamage();
        MiniJump();
      }
    }
  }

  private void OnCollisionExit2D(Collision2D others)
  {
    // Do something
  }

  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Damage"))
    {
      if (!PlayerManager.Instance.isIntangible)
      {
        PlayerManager.Instance.TakeDamage();
        MiniJump();
      }
    }
  }
}
