using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager gameManager;
	private Collider2D[] _objectsInRange = new Collider2D[10];
	private BoxCollider2D _playerActionRange;

	public ContactFilter2D filter;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_playerActionRange = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
		// Gets all objects in range whenever the player left clicks. If the clicked object is interactable, interact with it
		if(Input.GetMouseButtonDown(0))
		{
			_playerActionRange.OverlapCollider(filter, _objectsInRange);

			var clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var mousePosition2D = new Vector2(clickPosition.x, clickPosition.y);

			var hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
			if(hit.collider != null && IsObjectColliding(hit.collider))
			{
				var clickedObject = hit.collider.gameObject.GetComponent<IInteractable>();
				Debug.Log("CLICKED ON " + hit.collider.name);

				if(clickedObject != null)
				{
					clickedObject.Interact();
					Debug.Log("INTERACTING WITH " + hit.collider.name);
				}
			}
		}
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
}
