using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicoAttack : MonoBehaviour
{
  public GameObject spherePrefab;
  public List<GameObject> spherePoints;
  public float rotationSpeed = 100f;
  GameObject sphereParent;

  void Start()
  {
    sphereParent = spherePoints[0].transform.parent.gameObject;
  }

  // Update is called once per frame
  void Update()
  {
    Attack();
  }

  void Attack()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      if (!GameObject.FindGameObjectWithTag("Attack"))
      {
        GameObject sphere1 = Instantiate(spherePrefab, spherePoints[0].transform.position, Quaternion.identity);
        GameObject sphere2 = Instantiate(spherePrefab, spherePoints[1].transform.position, Quaternion.identity);
        GameObject sphere3 = Instantiate(spherePrefab, spherePoints[2].transform.position, Quaternion.identity);
        GameObject sphere4 = Instantiate(spherePrefab, spherePoints[3].transform.position, Quaternion.identity);

        sphere1.transform.SetParent(spherePoints[0].transform);
        sphere2.transform.SetParent(spherePoints[1].transform);
        sphere3.transform.SetParent(spherePoints[2].transform);
        sphere4.transform.SetParent(spherePoints[3].transform);

        StartCoroutine(RotateSphereParent());
      }
    }
  }

  IEnumerator RotateSphereParent()
  {
    while (true)
    {
      // Rotacionar em torno do eixo Z
      sphereParent.transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
      // Aguardar o próximo frame
      yield return null;
    }
  }

  public void FlipSphereParent()
  {
    sphereParent.transform.Rotate(0f, 180f, 0f);
  }
}
