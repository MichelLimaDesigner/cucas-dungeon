using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaGuaranaAttack : MonoBehaviour
{
  public float speed;

  // Método para configurar a direção do tiro com base na escala do inimigo
  public void SetDirection(Vector3 enemyScale)
  {
    // Definir a direção do tiro com base na escala do inimigo
    Vector3 shootDirection = transform.right + Vector3.up;
    shootDirection = new Vector3(shootDirection.x * enemyScale.x, shootDirection.y, shootDirection.z);

    // Mover o tiro na direção ajustada
    GetComponent<Rigidbody2D>().AddForce(shootDirection * speed, ForceMode2D.Impulse);
  }
}
