using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;

    float boundX = 0.75f;
    float boundY = 0.75f;

    public SpriteRenderer backgroundBounds;

    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;

    //Update after char finishes moving

    private void Start ()
    {
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        leftBound = horzExtent - backgroundBounds.sprite.bounds.size.x / 2.0f;
        rightBound = backgroundBounds.sprite.bounds.size.x / 2.0f - horzExtent;
        bottomBound = vertExtent - backgroundBounds.sprite.bounds.size.y / 2.0f;
        topBound = backgroundBounds.sprite.bounds.size.y / 2.0f - vertExtent;
    }
    private void LateUpdate ()
    {
        Vector3 delta = Vector3.zero;

        // This is to check if we're inside the bounds on the X axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        // This is to check if we're inside the bounds on the Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }
        //transform.position += new Vector3(delta.x, delta.y, 0);

        Vector3 newPosition = transform.position + new Vector3 (delta.x, delta.y, 0);

        newPosition.x = Mathf.Clamp (newPosition.x, leftBound, rightBound);
        newPosition.y = Mathf.Clamp (newPosition.y, bottomBound, topBound);

        transform.position = newPosition;
    }
}