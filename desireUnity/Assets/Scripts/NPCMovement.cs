using System;
using UnityEngine;

public class NPCMovement : MovementBase
{
	public Direction direction;
	private SpriteRenderer spriteRenderer;

	//Movement
	public bool moving;

	private Vector2 objective;
	private float moveSpeed;
	private float defaultSpeed = 2;
	private Animator animator;

	public bool canMove;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		//Movement
		objective = transform.position;
		moveSpeed = 0;
		canMove = true;

		if(animator)
		{
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
		}

		UpdateSizeForDepth(spriteRenderer);
	}

	private void Update()
	{
		UpdateSizeForDepth(spriteRenderer);
	}

	private void FixedUpdate()
	{
		if(!animator)
		{
			return;
		}

		if(moving && (Vector2) transform.position != objective)
		{
			float step = moveSpeed * Time.fixedDeltaTime;

			//Allow only rotation
			if(!canMove)
			{
				step = 0;
			}

			transform.position = Vector2.MoveTowards(transform.position, objective, step);

			animator.SetFloat("Speed", step);

			float xDifference = Math.Abs(transform.position.x - objective.x);
			float yDifference = Math.Abs(transform.position.y - objective.y);

			if(yDifference > xDifference)
			{
				if(objective.y > transform.position.y)
				{
					animator.SetFloat("Vertical", 1);
					animator.SetFloat("Horizontal", 0);
				}
				if(objective.y < transform.position.y)
				{
					animator.SetFloat("Vertical", -1);
					animator.SetFloat("Horizontal", 0);
				}
			}
			else
			{
				if(objective.x > transform.position.x)
				{
					animator.SetFloat("Horizontal", 1);
					animator.SetFloat("Vertical", 0);
				}
				if(objective.x < transform.position.x)
				{
					animator.SetFloat("Horizontal", -1);
					animator.SetFloat("Vertical", 0);
				}
			}
			//Stop continous movement
			if(!canMove)
			{
				objective = transform.position;
			}
		}
		else
		{
			moving = false;
			animator.SetFloat("Speed", 0);
		}
	}

	private void UpdateDirection()
	{
		if(!animator)
		{
			return;
		}
		if(animator.GetFloat("Horizontal") == 1)
		{
			direction = Direction.Right;
		}
		if(animator.GetFloat("Horizontal") == -1)
		{
			direction = Direction.Left;
		}
		if(animator.GetFloat("Vertical") == 1)
		{
			direction = Direction.Top;
		}
		if(animator.GetFloat("Vertical") == -1)
		{
			direction = Direction.Bottom;
		}
	}

	public void MoveTo(Vector2 newObjective)
	{
		objective = newObjective;
		moveSpeed = defaultSpeed;
		moving = true;
	}
}