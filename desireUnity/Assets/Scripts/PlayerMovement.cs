using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : ActorsMovement
{
    public Direction direction = new Direction();
    public float moveSpeed = 2f;
    public Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private RaycastHit2D hit;
    Vector2 movement;
    private GameManager gameManager;

    private Vector3 previousPosition;

    private GridLayout grid;

    private bool axisMoved;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        previousPosition = transform.position;

        //Order in layer according to cell
        //grid = GameObject.Find("Grid").GetComponent<GridLayout>();
        //int newOrderInLayer = grid.WorldToCell(transform.position).y * -1;
        //spriteRenderer.sortingOrder = newOrderInLayer;

        switch (direction)
        {
            case Direction.top:
                animator.SetFloat("Vertical", 1);
                break;
            case Direction.right:
                animator.SetFloat("Horizontal", 1);
                break;
            case Direction.bottom:
                animator.SetFloat("Vertical", -1);
                break;
            case Direction.left:
                animator.SetFloat("Horizontal", -1);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            movement.x = 0;
            movement.y = 0;
            animator.SetFloat("Speed", 0);
            axisMoved = false;
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // if (grid.WorldToCell(transform.position).y != grid.WorldToCell(previousPosition).y)
        // {
        //     int newOrderInLayer = grid.WorldToCell(transform.position).y * -1;
        //     spriteRenderer.sortingOrder = newOrderInLayer;
        //     previousPosition = transform.position;
        // }
        CheckAxisMoved();
    }
    private void FixedUpdate()
    {
        transform.Translate(0, movement.y * moveSpeed * Time.fixedDeltaTime, 0);
        transform.Translate(movement.x * moveSpeed * Time.fixedDeltaTime, 0, 0);
    }

    private void UpdateDirection()
    {
        if (animator.GetFloat("Horizontal") == 1)
        {
            direction = Direction.right;
        }
        if (animator.GetFloat("Horizontal") == -1)
        {
            direction = Direction.left;
        }
        if (animator.GetFloat("Vertical") == 1)
        {
            direction = Direction.top;
        }
        if (animator.GetFloat("Vertical") == -1)
        {
            direction = Direction.bottom;
        }
    }

    private void CheckAxisMoved()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            if (axisMoved == false)
            {
                UpdateDirection();
                axisMoved = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            if (axisMoved == false)
            {
                UpdateDirection();
                axisMoved = true;
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            if (axisMoved == false)
            {
                UpdateDirection();
                axisMoved = true;
            }
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            if (axisMoved == false)
            {
                UpdateDirection();
                axisMoved = true;
            }
        }
    }
}
