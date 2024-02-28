using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
  public bool isActive = false;
  SpriteRenderer renderer;

  void Start()
  {
    renderer = GetComponent<SpriteRenderer>();
    renderer.color = new Color(0, 0, 0, 1);
  }

  void OnTriggerEnter2D(Collider2D others)
  {
    if (!isActive && others.CompareTag("Player"))
    {
      isActive = true;
      renderer.color = new Color(52, 197, 44, 255);
      GameManager.Instance.HandleLevelPortal();
    }
  }
}
