using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
  public bool isActive = false;
  public Animator animator;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && GameManager.Instance.canPassLevel)
    {
      isActive = true;
      animator.SetBool("isActive", true);
      StartCoroutine(FinishLevel());
    }
  }

  IEnumerator FinishLevel()
  {
    GameManager.Instance.HandleFinishStage();
    yield return new WaitForSeconds(3);
  }
}
