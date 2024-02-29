using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
  public Animator animator;
  public static LevelTransition Instance;

  private void Awake()
  {
    Instance = this;
  }

  public void Transition()
  {
    animator.SetTrigger("Start");
  }
}
