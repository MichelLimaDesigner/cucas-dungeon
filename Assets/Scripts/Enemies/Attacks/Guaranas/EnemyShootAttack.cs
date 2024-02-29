using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : MonoBehaviour
{
  public GameObject shootPrefab;
  public float shootInterval;
  public float rotationSpeed = 5f; // Velocidade de rotação do inimigo
  public Transform spawnPoint;
  public float attackRadius;

  private bool canAttack = true;

  void Update()
  {
    CanAttack();
  }

  void CanAttack()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, attackRadius);

    foreach (Collider2D collider in colliders)
    {
      if (collider.CompareTag("Player") && canAttack)
      {
        // Determine a direção do jogador em relação ao inimigo
        Vector3 directionToPlayer = collider.transform.position - transform.position;

        // Atualize a orientação do inimigo com base na direção do jogador
        FlipToPlayer(directionToPlayer.x);

        StartCoroutine(Attack());
        canAttack = false;
      }
    }
  }

  void FlipToPlayer(float directionX)
  {
    // Verifique a direção do jogador e vire o inimigo para a esquerda ou direita
    if (directionX > 0)
    {
      // Jogador à direita do inimigo
      transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); // Manter a escala original
    }
    else if (directionX < 0)
    {
      // Jogador à esquerda do inimigo
      transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f); // Inverter a escala na direção X
    }
  }

  IEnumerator Attack()
  {
    GameObject shoot = Instantiate(shootPrefab, spawnPoint.position, Quaternion.identity);
    shoot.GetComponent<PlantaGuaranaAttack>().SetDirection(transform.localScale);

    yield return new WaitForSeconds(shootInterval);

    canAttack = true;
  }

}