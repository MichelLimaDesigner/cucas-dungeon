using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyPrefab : MonoBehaviour
{
  public int timeToDestroy;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(AttackDuration(timeToDestroy));
  }

  IEnumerator AttackDuration(int time)
  {
    yield return new WaitForSeconds(time);
    Destroy(gameObject);
  }
}
