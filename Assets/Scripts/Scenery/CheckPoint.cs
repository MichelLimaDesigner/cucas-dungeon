using UnityEngine;

public class CheckPoint : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    var player = other.GetComponent<Player>();

    if (player)
    {
      player.SetRespawnPoint(transform.position);
    }
  }
}
