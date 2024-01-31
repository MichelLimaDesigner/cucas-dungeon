using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
  public Transform portalDestination;
  public bool isHeightPortal;
  public List<GameObject> objectsToTeleport = new List<GameObject>();
  public List<string> allowedTags = new List<string>();
  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log(other.gameObject);
    // Verifica se o objeto que colidiu com o portal está na lista de objetos permitidos
    if (allowedTags.Contains(other.tag))
    {
      TeleportObject(other.gameObject);
    }
  }

  private void TeleportObject(GameObject objToTeleport)
  {

    Player playerScript = objToTeleport.GetComponent<Player>();

    if (Vector2.Distance(objToTeleport.transform.position, transform.position) > 0.3f)
    {
      if (isHeightPortal) objToTeleport.transform.position = portalDestination.position;
      else
      {
        Debug.Log("aquiiiiii");
        Debug.Log(objToTeleport);
        var position = portalDestination.position;
        // if (playerScript.isFacingRight) position.x += 1f;
        // else position.x -= 1f;
        objToTeleport.transform.position = position;
      }
    }
  }
}
