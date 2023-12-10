using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
  public Transform portalDestination;
  public bool isHeightPortal;
  private Player playerScript;
  GameObject player;

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    playerScript = player.GetComponent<Player>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
      {
        if (isHeightPortal) player.transform.position = portalDestination.transform.position;
        else
        {
          var position = portalDestination.transform.position;
          if (playerScript.isFacingRight) position.x += 1f;
          else position.x -= 1f;
          player.transform.position = position;
        }

      }
    }
  }
}
