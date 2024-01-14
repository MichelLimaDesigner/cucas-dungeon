using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaAttack : MonoBehaviour
{
  private bool canShoot = true;
  public GameObject bombPotionPrefab;
  public Transform firePoint;

  // Update is called once per frame
  void Update()
  {
    Attack();
  }

  void Attack()
  {
    if (Input.GetKeyDown(KeyCode.C) && canShoot)
    {
      canShoot = false;
      Instantiate(bombPotionPrefab, firePoint.position, firePoint.rotation);
      StartCoroutine(SetCanShoot(1));
    }
  }

  IEnumerator SetCanShoot(int time)
  {
    yield return new WaitForSeconds(time);

    canShoot = true;
  }
}
