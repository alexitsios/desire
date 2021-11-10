using System;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
	public Direction direction;
	public Animator animator;
	private SpriteRenderer spriteRenderer;
	private GameManager gameManager;
	public Transform minScale;
	public Transform maxScale;

	private bool lookingForThis;

	//Movement
	bool moving;
	private Vector2 objective;
	private float moveSpeed;
	public bool canMove;

	//Conversation
	public bool blockLoop;
	public float totalBlocks;
	private float currentBlock = 0;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		lookingForThis = false;

		//Movement
		objective = transform.position;
		moveSpeed = 0;
		canMove = false;

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
		UpdateSizeForDepth();
	}

	private void Update()
	{
		UpdateSizeForDepth();
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

	private void UpdateSizeForDepth()
	{
		float scaleValue = Mathf.Lerp(minScale.localScale.y, maxScale.localScale.y, Mathf.InverseLerp(minScale.position.y, maxScale.position.y, transform.position.y));
		transform.localScale = new Vector3(scaleValue, scaleValue, 0);

		spriteRenderer.sortingOrder = (int) (transform.position.y * -1);
	}

	private void StartConversation()
	{
		string dialogueId = gameObject.name + "_" + currentBlock;
		gameManager.StartConversation(dialogueId);
		if(currentBlock < totalBlocks)
		{
			currentBlock++;
		}
		else if(currentBlock == totalBlocks && blockLoop)
		{
			currentBlock = 0;
		}

	}

	public void GoTo(Vector2 newObjective, float speed)
	{
		objective = newObjective;
		moveSpeed = speed;
		moving = true;
	}
}