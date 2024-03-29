using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PombaAttack : MonoBehaviour
{
  private bool canShoot = true;
  public GameObject bulletPrefab;
  public Transform firePoint;
  AudioManager audioManager;

  void Awake()
  {
    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
  }

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
      Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
      audioManager.PlaySFX(audioManager.pombaSFX);
      StartCoroutine(SetCanShoot(1));
    }
  }

  IEnumerator SetCanShoot(int time)
  {
    yield return new WaitForSeconds(time);

    canShoot = true;
  }
}
