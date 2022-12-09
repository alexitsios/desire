using UnityEngine;

public class StopMovement : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.StopMovement();
        }
    }
}
