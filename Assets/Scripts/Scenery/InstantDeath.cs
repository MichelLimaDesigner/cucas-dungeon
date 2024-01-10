using UnityEngine;

public class InstantDeath : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    var player = other.collider.GetComponent<Player>();
    if (player)
    {
      Debug.Log(other);
      // player.Die();
      // player.TakeDamage();
    }
  }
}
