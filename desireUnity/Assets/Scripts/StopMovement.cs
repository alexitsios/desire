using UnityEngine;

public class StopMovement : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out var player))
        {
            player.StopMovement();
        }
    }
}
