using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
  public bool isActive = false;
  SpriteRenderer renderer;

  void Start()
  {
    renderer = GetComponent<SpriteRenderer>();
    renderer.color = new Color(0, 0, 0, 1);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && GameManager.Instance.canPassLevel)
    {
      isActive = true;
      renderer.color = new Color(52, 197, 44, 255);
      StartCoroutine(FinishLevel());
    }
  }

  IEnumerator FinishLevel()
  {
    GameManager.Instance.HandleFinishStage();
    yield return new WaitForSeconds(3);
  }
}
