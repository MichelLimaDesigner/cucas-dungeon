using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorcegoAttack : MonoBehaviour
{
  private bool canShoot = true;
  public GameObject bulletPrefab;
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
      bulletPrefab.SetActive(true);
      audioManager.PlaySFX(audioManager.morcegoSFX);
      StartCoroutine(AttackDuration(1));
      StartCoroutine(SetCanShoot(3));
    }
  }

  IEnumerator SetCanShoot(int time)
  {
    yield return new WaitForSeconds(time);

    canShoot = true;
  }

  IEnumerator AttackDuration(int time)
  {
    yield return new WaitForSeconds(time);

    bulletPrefab.SetActive(false);
  }
}
