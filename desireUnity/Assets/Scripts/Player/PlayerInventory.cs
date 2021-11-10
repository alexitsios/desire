using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<ItemBase> inventory;
    private ItemsMenu inventoryScreen;

	public void Start()
	{
        inventoryScreen = GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>();
	}

	/// <summary>
	///     Adds a new item to the player's inventory
	/// </summary>
	public void AddItem(ItemBase item)
    {
        inventory.Add(item);

        inventoryScreen.UpdateInventoryScreen(inventory);
    }

    /// <summary>
    ///     Removes an item from the player's inventory
    /// </summary>
    public void RemoveItem(ItemType itemType)
    {
        var item = inventory.Where(i => i.itemType == itemType).ToList()[0];
        inventory.Remove(item);

        inventoryScreen.UpdateInventoryScreen(inventory);
    }
}
