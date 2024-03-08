using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucasIntro : MonoBehaviour
{
  public Animator animator;
  public float animationTime;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(Intro());
  }

  IEnumerator Intro()
  {
    yield return new WaitForSeconds(animationTime);
    animator.SetTrigger("finishIntro");
    StartCoroutine(DisableCanvas());
  }

  IEnumerator DisableCanvas()
  {
    yield return new WaitForSeconds(1.5f);
    gameObject.SetActive(false);
  }
}
