using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MovementBase
{
	public Direction direction = new Direction();
	public float frontMoveSpeed;
	public bool IsTrapped { get; set; }
	public Animator animator;

	private PlayerInteraction playerInteraction;
	private float moveSpeed;
	private SpriteRenderer spriteRenderer;
	private Vector2 lastClickedPos;
	private bool moving;
	private Vector3 lastPosition = Vector3.zero;

	public float step;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerInteraction = GetComponent<PlayerInteraction>();

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
			// Only moves the character if the player has not clicked on an inventory item
			if(EventSystem.current.currentSelectedGameObject == null)
			{
				lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				moving = true;
			}
		}

		UpdateSizeForDepth(spriteRenderer);
	}
	private void FixedUpdate()
	{
		if(moving && (Vector2) transform.position != lastClickedPos)
		{
			step = moveSpeed * Time.fixedDeltaTime;

			//Allow only rotation
			if(IsTrapped)
			{
				step = 0;
			}

			transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
			lastPosition = transform.position;

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
			if(IsTrapped)
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
}