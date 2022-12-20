using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MovementBase
{
	public Direction direction;
	public float frontMoveSpeed;
	public bool IsTrapped
	{
		get => _isTrapped;
		set
		{
			_isTrapped = value;

			if(_isTrapped)
			{
				GetComponent<SpriteRenderer>().size = new Vector2(0.5f, 0.5f);
				GetComponent<Animator>().SetBool("acquired_leg", false);
				GetComponent<Animator>().SetBool("acquired_arm", false);
			}
			else
			{
				GetComponent<SpriteRenderer>().size = new Vector2(0.25f, 0.5f);
				GetComponent<Animator>().SetBool("acquired_leg", true);
				transform.GetChild(0).transform.localPosition = new Vector3(0, -0.15f, 0);
			}
		}
	}
	public bool AcquiredArm
	{
		get => _acquiredArm;
		set
		{
			_acquiredArm = value;

			if(_acquiredArm)
			{
				GetComponent<Animator>().SetBool("acquired_arm", true);
			}
		}
	}

	public Animator animator;

	private PlayerInteraction playerInteraction;
	private float moveSpeed;
	private SpriteRenderer spriteRenderer;
	private Vector2 lastClickedPos;
	private bool moving;
	private bool _isTrapped;
	private bool _acquiredArm;
	private float _idleTimer = 0f;

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
		_idleTimer += Time.deltaTime;

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
				_idleTimer = 0f;
				animator.SetBool("canIdle", true);
				lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				moving = true;
			}
		}

		//Hold to move
		if(Input.GetMouseButton(0) && !playerInteraction.isInteracting)
		{
			// Only moves the character if the player has not clicked on an inventory item
			if(EventSystem.current.currentSelectedGameObject == null)
			{
				_idleTimer = 0f;
				animator.SetBool("canIdle", true);
				lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				moving = true;
			}
		}

		if(_idleTimer >= 5f)
		{
			_idleTimer = 0f;
			animator.SetBool("canIdle", true);
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

			animator.SetFloat("Speed", step);

			var xDifference = Math.Abs(transform.position.x - lastClickedPos.x);
			var yDifference = Math.Abs(transform.position.y - lastClickedPos.y);

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

					if(AcquiredArm)
					{
						GetComponent<SpriteRenderer>().flipX = false;
					}
				}

				if(lastClickedPos.x < transform.position.x)
				{
					animator.SetFloat("Horizontal", -1);
					animator.SetFloat("Vertical", 0);

					if(AcquiredArm)
					{
						GetComponent<SpriteRenderer>().flipX = true;
					}
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

	public void StartBootAnimation()
	{
		animator.speed = 1;
	}

	public void StopMovement()
	{
		lastClickedPos = transform.position;
	}
}
