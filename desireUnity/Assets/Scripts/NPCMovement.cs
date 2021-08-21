using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCMovement : ActorsMovement
{
    public Direction direction = new Direction();
    public Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    public Transform minScale;
    public Transform maxScale;
    public BoxCollider2D ActionRange;
    public BoxCollider2D ClickArea;
    private bool inActionRange;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];
    private bool lookingForThis;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inActionRange = false;
        lookingForThis = false;

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
        UpdateSizeForDepth();
    }
    private void Update()
    {
        //Highlight NPC/Item
        if (lookingForThis)
        {
            spriteRenderer.color = Color.green;
        }

        //Init Conversation
        if (inActionRange && lookingForThis && !gameManager.inConversation)
        {
            lookingForThis = false;
            spriteRenderer.color = Color.white;
            gameManager.StartConversation(gameObject.name);
            return;
        }

        //Get input
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (ClickArea.OverlapPoint(mousePosition))
            {
                lookingForThis = true;
            }
            else
            {
                lookingForThis = false;
                spriteRenderer.color = Color.white;
            }
        }

        ActionRange.OverlapCollider(filter, hits);
        inActionRange = CheckCollisions();
    }

    private void FixedUpdate()
    {
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

    private void UpdateSizeForDepth()
    {
        float scaleValue = Mathf.Lerp(minScale.localScale.y, maxScale.localScale.y, Mathf.InverseLerp(minScale.position.y, maxScale.position.y, transform.position.y));
        transform.localScale = new Vector3(scaleValue, scaleValue, 0);
    }
    protected bool CheckCollisions()
    {
        bool felt = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            if (hits[i].name == "Player")
            {
                felt = true;
            }
            hits[i] = null;
        }
        return felt;
    }

    void OnMouseOver()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (ClickArea.OverlapPoint(mousePosition) && !lookingForThis && !gameManager.inConversation)
        {
            spriteRenderer.color = Color.blue;
        }
    }

    void OnMouseExit()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!ClickArea.OverlapPoint(mousePosition) && !lookingForThis && !gameManager.inConversation)
        {
            spriteRenderer.color = Color.white;
        }
    }
}