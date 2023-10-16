using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
  private Vector3 originalScale;
  private bool isScaling = false;

  // Start is called before the first frame update
  void Start()
  {
    originalScale = transform.localScale;
  }

  // -------------------- Collision functions
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && !isScaling)
    {
      StartCoroutine(ScaleOverTime(originalScale, new Vector3(0.1f, 0.1f, 0.1f), 0.5f));
    }
  }

  private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration)
  {
    isScaling = true;
    float startTime = Time.time;

    while (Time.time - startTime < duration)
    {
      float progress = (Time.time - startTime) / duration;
      transform.localScale = Vector3.Lerp(startScale, endScale, progress);
      yield return null;
    }

    transform.localScale = endScale;
    isScaling = false;
    Destroy(gameObject);
  }
}
