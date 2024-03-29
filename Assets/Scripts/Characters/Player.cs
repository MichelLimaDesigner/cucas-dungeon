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
  [Header("Ground and wall system")]
  private bool isGrounded;
  private bool isWallTouch;
  private bool isSliding;
  public float wallSlidingSpeed;
  public Transform groundCheck;
  public Transform wallCheck;
  public LayerMask groundLayer;
  public float wallJumpDuration;
  public Vector2 wallJumpForce;
  bool wallJumping;


  // -------------------- Dashing properties
  [Header("Dashing")]
  public float dashingVelocity = 5f;
  public float dashingTime = 0.1f;
  public TrailRenderer trailRenderer;
  private Vector2 dashingDir;
  private bool isDashing = false;
  private bool canDash = true;

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
    trailRenderer = GetComponent<TrailRenderer>();
  }

  // -------------------- Update is called once per frame
  void Update()
  {
    // -------------------- Variables
    move = Input.GetAxis("Horizontal");
    jump = Input.GetButtonUp("Jump");
    isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.3f, 0.1f), 0, groundLayer);
    isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.1f, 0.4f), 0, groundLayer);

    if (isWallTouch && !isGrounded && move != 0) isSliding = true;
    else isSliding = false;
    // -------------------- Methods
    Flip();
    Jump();
    Dash();
    WallSliding();

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
      else if (isSliding)
      {
        wallJumping = true;
        Invoke("StopWallJump", wallJumpDuration);
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

    if (wallJumping)
    {
      rig.velocity = new Vector2(-move * wallJumpForce.x, wallJumpForce.y);
    }

  }

  void StopWallJump()
  {
    wallJumping = false;
  }

  // -------------------- Wall Sligins

  void WallSliding()
  {
    if (isSliding)
    {
      rig.velocity = new Vector2(rig.velocity.x, Mathf.Clamp(rig.velocity.y, -wallSlidingSpeed, float.MaxValue));
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
    // Player is on ground
    if (others.gameObject.layer == 6)
    {
      if (!isDashing) canDash = true;
    }
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
    if (other.CompareTag("EnemyHeadHit"))
    {
      MiniJump();
    }
    else if (other.CompareTag("Damage"))
    {
      if (!PlayerManager.Instance.isIntangible)
      {
        PlayerManager.Instance.TakeDamage();
        MiniJump();
      }
    }
  }
}
