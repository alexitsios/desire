using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ActorsMovement
{
    public Direction direction = new Direction ();
    public float frontMoveSpeed;
    private float moveSpeed;
    public Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private Vector3 previousPosition;
    public Transform minScale;
    public Transform maxScale;
    private Vector2 lastClickedPos;
    private Rigidbody2D rigidBody;
    bool moving;

    public bool trapped;

    private void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        previousPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D> ();

        moveSpeed = frontMoveSpeed;

        switch (direction)
        {
            case Direction.top:
                animator.SetFloat ("Vertical", 1);
                break;
            case Direction.right:
                animator.SetFloat ("Horizontal", 1);
                break;
            case Direction.bottom:
                animator.SetFloat ("Vertical", -1);
                break;
            case Direction.left:
                animator.SetFloat ("Horizontal", -1);
                break;
            default:
                break;
        }
        UpdateSizeForDepth ();
    }
    private void Update ()
    {
        if (gameManager.inConversation)
        {
            lastClickedPos = transform.position;
            animator.SetFloat ("Speed", 0);
            return;
        }

        //Point to move
        if (Input.GetMouseButtonDown (0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            moving = true;
        }
        UpdateSizeForDepth ();
    }
    private void FixedUpdate ()
    {
        if (moving && (Vector2) transform.position != lastClickedPos)
        {

            float step = moveSpeed * Time.fixedDeltaTime;

            //Allow only rotation
            if (trapped)
            {
                step = 0;
            }

            transform.position = Vector2.MoveTowards (transform.position, lastClickedPos, step);

            animator.SetFloat ("Speed", step);

            float xDifference = Math.Abs (transform.position.x - lastClickedPos.x);
            float yDifference = Math.Abs (transform.position.y - lastClickedPos.y);

            if (yDifference > xDifference)
            {
                if (lastClickedPos.y > transform.position.y)
                {
                    animator.SetFloat ("Vertical", 1);
                    animator.SetFloat ("Horizontal", 0);
                }
                if (lastClickedPos.y < transform.position.y)
                {
                    animator.SetFloat ("Vertical", -1);
                    animator.SetFloat ("Horizontal", 0);
                }
            }
            else
            {
                if (lastClickedPos.x > transform.position.x)
                {
                    animator.SetFloat ("Horizontal", 1);
                    animator.SetFloat ("Vertical", 0);
                }
                if (lastClickedPos.x < transform.position.x)
                {
                    animator.SetFloat ("Horizontal", -1);
                    animator.SetFloat ("Vertical", 0);
                }
            }

            //Stop continous movement
            if (trapped)
            {
                lastClickedPos = transform.position;
            }
        }
        else
        {
            moving = false;
            animator.SetFloat ("Speed", 0);
        }
    }

    private void UpdateSizeForDepth ()
    {
        float scaleValue = Mathf.Lerp (minScale.localScale.y, maxScale.localScale.y, Mathf.InverseLerp (minScale.position.y, maxScale.position.y, transform.position.y));
        transform.localScale = new Vector3 (scaleValue, scaleValue, 0);
        moveSpeed = scaleValue * frontMoveSpeed / maxScale.localScale.y;

        spriteRenderer.sortingOrder = (int) (transform.position.y * -1);
    }

    public void ToogleTrapped ()
    {
        trapped = !trapped;
    }
}