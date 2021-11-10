using System;
using UnityEngine;

public class PlayerMovement : MovementBase
{
	public Direction direction = new Direction();
	public float frontMoveSpeed;
	public bool trapped;
	public Animator animator;

	private PlayerInteraction playerInteraction;
	private float moveSpeed;
	private SpriteRenderer spriteRenderer;
	private GameManager gameManager;
	private Vector3 previousPosition;
	private Vector2 lastClickedPos;
	private bool moving;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		playerInteraction = GetComponent<PlayerInteraction>();
		previousPosition = transform.position;

		moveSpeed = frontMoveSpeed;

		switch(direction)
		{
			case Direction.Top:
				animator.SetFloat("Vertical", 1);
				break;
			case Direction.Right:
				animator.SetFloat("Horizontal", 1);
				break;
			case Direction.Bottom:
				animator.SetFloat("Vertical", -1);
				break;
			case Direction.Left:
				animator.SetFloat("Horizontal", -1);
				break;
			default:
				break;
		}
		UpdateSizeForDepth(spriteRenderer);
	}
	private void Update()
	{
		if(playerInteraction.isInteracting)
		{
			lastClickedPos = transform.position;
			animator.SetFloat("Speed", 0);
			return;
		}

		//Point to move
		if(Input.GetMouseButtonDown(0) && !playerInteraction.isInteracting)
		{
			lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			moving = true;
		}

		UpdateSizeForDepth(spriteRenderer);
	}
	private void FixedUpdate()
	{
		if(moving && (Vector2) transform.position != lastClickedPos)
		{

			float step = moveSpeed * Time.fixedDeltaTime;

			//Allow only rotation
			if(trapped)
			{
				step = 0;
			}

			transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);

			animator.SetFloat("Speed", step);

			float xDifference = Math.Abs(transform.position.x - lastClickedPos.x);
			float yDifference = Math.Abs(transform.position.y - lastClickedPos.y);

			if(yDifference > xDifference)
			{
				if(lastClickedPos.y > transform.position.y)
				{
					animator.SetFloat("Vertical", 1);
					animator.SetFloat("Horizontal", 0);
				}
				if(lastClickedPos.y < transform.position.y)
				{
					animator.SetFloat("Vertical", -1);
					animator.SetFloat("Horizontal", 0);
				}
			}
			else
			{
				if(lastClickedPos.x > transform.position.x)
				{
					animator.SetFloat("Horizontal", 1);
					animator.SetFloat("Vertical", 0);
				}
				if(lastClickedPos.x < transform.position.x)
				{
					animator.SetFloat("Horizontal", -1);
					animator.SetFloat("Vertical", 0);
				}
			}

			//Stop continous movement
			if(trapped)
			{
				lastClickedPos = transform.position;
			}
		}
		else
		{
			moving = false;
			animator.SetFloat("Speed", 0);
		}
	}

	public void ToogleTrapped()
	{
		trapped = !trapped;
	}
}