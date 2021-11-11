using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> inventory = new List<InventoryItem>();
    private ItemsMenu inventoryScreen;

	public void Start()
	{
        inventoryScreen = GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>();
	}

	/// <summary>
	///     Adds a new item to the player's inventory
	/// </summary>
	public void AddItem(InventoryItem item)
    {
        Debug.Log("ADDING ITEM " + item._type.ToString());
        inventory.Add(item);

        inventoryScreen.UpdateInventoryScreen(inventory);
    }

    /// <summary>
    ///     Removes an item from the player's inventory
    /// </summary>
    public void RemoveItem(ItemType itemType)
    {
        Debug.Log("REMOVING ITEM " + itemType.ToString());
        foreach(var item in inventory)
		{
            if(item._type == itemType)
			{
                inventory.Remove(item);
                break;
            }
		}

        inventoryScreen.UpdateInventoryScreen(inventory);
    }
}
