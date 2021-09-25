using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : ActorsMovement
{
    public Direction direction = new Direction ();
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

    //Movement
    bool moving;
    private Vector2 objective;
    private float moveSpeed;
    public bool toggleMovement;

    //Conversation
    public bool blockLoop;
    public float totalBlocks;
    private float currentBlock = 0;

    private void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        inActionRange = false;
        lookingForThis = false;

        //Movement
        objective = transform.position;
        moveSpeed = 0;
        toggleMovement = false;

        if (animator)
        {
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
        }
        UpdateSizeForDepth ();
    }

    private void Update ()
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
            StartConversation ();
            return;
        }

        //Get input
        if (Input.GetMouseButtonDown (0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            if (ClickArea.OverlapPoint (mousePosition))
            {
                lookingForThis = true;
            }
            else
            {
                lookingForThis = false;
                spriteRenderer.color = Color.white;
            }
        }

        ActionRange.OverlapCollider (filter, hits);
        inActionRange = CheckCollisions ();

        UpdateSizeForDepth ();
    }

    private void FixedUpdate ()
    {
        if (!animator)
        {
            return;
        }

        if (moving && (Vector2) transform.position != objective)
        {
            float step = moveSpeed * Time.fixedDeltaTime;

            //Allow only rotation
            if (!toggleMovement)
            {
                step = 0;
            }

            transform.position = Vector2.MoveTowards (transform.position, objective, step);

            animator.SetFloat ("Speed", step);

            float xDifference = Math.Abs (transform.position.x - objective.x);
            float yDifference = Math.Abs (transform.position.y - objective.y);

            if (yDifference > xDifference)
            {
                if (objective.y > transform.position.y)
                {
                    animator.SetFloat ("Vertical", 1);
                    animator.SetFloat ("Horizontal", 0);
                }
                if (objective.y < transform.position.y)
                {
                    animator.SetFloat ("Vertical", -1);
                    animator.SetFloat ("Horizontal", 0);
                }
            }
            else
            {
                if (objective.x > transform.position.x)
                {
                    animator.SetFloat ("Horizontal", 1);
                    animator.SetFloat ("Vertical", 0);
                }
                if (objective.x < transform.position.x)
                {
                    animator.SetFloat ("Horizontal", -1);
                    animator.SetFloat ("Vertical", 0);
                }
            }
            //Stop continous movement
            if (!toggleMovement)
            {
                objective = transform.position;
            }
        }
        else
        {
            moving = false;
            animator.SetFloat ("Speed", 0);
        }
    }

    private void UpdateDirection ()
    {
        if (!animator)
        {
            return;
        }
        if (animator.GetFloat ("Horizontal") == 1)
        {
            direction = Direction.right;
        }
        if (animator.GetFloat ("Horizontal") == -1)
        {
            direction = Direction.left;
        }
        if (animator.GetFloat ("Vertical") == 1)
        {
            direction = Direction.top;
        }
        if (animator.GetFloat ("Vertical") == -1)
        {
            direction = Direction.bottom;
        }
    }

    private void UpdateSizeForDepth ()
    {
        float scaleValue = Mathf.Lerp (minScale.localScale.y, maxScale.localScale.y, Mathf.InverseLerp (minScale.position.y, maxScale.position.y, transform.position.y));
        transform.localScale = new Vector3 (scaleValue, scaleValue, 0);

        spriteRenderer.sortingOrder = (int) (transform.position.y * -1);
    }

    private void StartConversation ()
    {
        string dialogueId = gameObject.name + "_" + currentBlock;
        gameManager.StartConversation (dialogueId);
        if (currentBlock < totalBlocks)
        {
            currentBlock++;
        }
        else if (currentBlock == totalBlocks && blockLoop)
        {
            currentBlock = 0;
        }

    }

    public void GoTo (Vector2 newObjective, float speed)
    {
        objective = newObjective;
        moveSpeed = speed;
        moving = true;
    }

    protected bool CheckCollisions ()
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

    void OnMouseOver ()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

        if (ClickArea.OverlapPoint (mousePosition) && !lookingForThis && !gameManager.inConversation)
        {
            spriteRenderer.color = Color.blue;
        }
    }

    void OnMouseExit ()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        if (!ClickArea.OverlapPoint (mousePosition) && !lookingForThis && !gameManager.inConversation)
        {
            spriteRenderer.color = Color.white;
        }
    }
}