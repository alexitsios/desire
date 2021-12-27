using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private readonly Collider2D[] _objectsInRange = new Collider2D[10];
	private BoxCollider2D _playerActionRange;
	private Collider2D _objectOutOfReach = null;
	private GameManager _gameManager;

	public ItemType _selectedItem;
	public ContactFilter2D filter;
	public bool isInteracting = true;

	private void Start()
	{
		_playerActionRange = GetComponent<BoxCollider2D>();
		_selectedItem = ItemType.NoItem;
		_gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	private void Update()
	{
		// If the player tried to interact with an object out of reach
		if(_objectOutOfReach != null)
		{
			Array.Clear(_objectsInRange, 0, _objectsInRange.Length);
			_playerActionRange.OverlapCollider(filter, _objectsInRange);

			// Interacts with the object when it's in reach
			if(IsObjectColliding(_objectOutOfReach))
			{
				_objectOutOfReach.gameObject.GetComponent<IInteractable>().Interact(_selectedItem);
				isInteracting = true;
				_objectOutOfReach = null;
				_selectedItem = ItemType.NoItem;
			}
		}

		// Gets all objects in range whenever the player left clicks. If the clicked object is interactable, interact with it
		if(Input.GetMouseButtonDown(0) && !isInteracting)
		{
			Array.Clear(_objectsInRange, 0, _objectsInRange.Length);
			_playerActionRange.OverlapCollider(filter, _objectsInRange);

			_gameManager.CanCursorChange = true;
			_gameManager.SetCursorAction(CursorAction.Pointer);

			_objectOutOfReach = null;

			var clickPosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

			var hit = Physics2D.Raycast(clickPosition, Vector2.zero);
			if(hit.collider != null)
			{
				var clickedObject = hit.collider.gameObject.GetComponent<IInteractable>();

				// If the clicked object is in reach
				if(clickedObject != null && IsObjectColliding(hit.collider))
				{
					clickedObject.Interact(_selectedItem);
					isInteracting = true;
					SetSelectedItem(ItemType.NoItem);
				}
				// If the clicked object is out of reach
				else if(clickedObject != null)
				{
					_objectOutOfReach = hit.collider;
				}
			}

			if(_objectOutOfReach == null)
				_selectedItem = ItemType.NoItem;
		}
	}

	public void FinishInteraction()
	{
		isInteracting = false;
	}

	// Checks if the informed collider is currently colliding with the player's Interacting Area
	private bool IsObjectColliding(Collider2D hit)
	{
		foreach(var collider in _objectsInRange)
		{
			if(collider == hit)
				return true;
		}

		return false;
	}

	public void SetSelectedItem(ItemType item)
	{
		_selectedItem = item;
	}
}
