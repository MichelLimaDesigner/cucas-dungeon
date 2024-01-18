using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureController : MonoBehaviour
{
  public PlayerScriptable character;
  public PlayerController controller;

  void Start()
  {
    controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();
  }

  // -------------------- Trigger functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      controller.AddCharacter(character);
      Destroy(gameObject);
    }
  }
}
