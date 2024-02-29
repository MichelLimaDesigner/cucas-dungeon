using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CururuAttack : MonoBehaviour
{
  private bool canShoot = true;
  public GameObject bulletPrefab;
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
      Instantiate(bulletPrefab, firePoint.position, bulletPrefab.transform.rotation);
      StartCoroutine(SetCanShoot(2));
    }
  }

  IEnumerator SetCanShoot(int time)
  {
    yield return new WaitForSeconds(time);
    canShoot = true;
  }
}
