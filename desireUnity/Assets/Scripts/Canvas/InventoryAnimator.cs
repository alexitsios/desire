using UnityEngine;

public class InventoryAnimator : MonoBehaviour
{
	private Animator _inventoryAnimator;

	private void Start()
	{
		_inventoryAnimator = GetComponent<Animator>();
	}

	public void ToggleInventory()
	{
		Debug.Log("Click");
		_inventoryAnimator.SetBool("is_open", !_inventoryAnimator.GetBool("is_open"));
	}
}
