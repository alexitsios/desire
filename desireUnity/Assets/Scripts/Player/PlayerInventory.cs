using System.Collections.Generic;
using System.Linq;
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

        //inventoryScreen.UpdateInventoryScreen(inventory);
    }

    /// <summary>
    ///     Removes an item from the player's inventory
    /// </summary>
    public void RemoveItem(ItemType itemType)
    {
        var item = inventory.Where(i => i._type == itemType).ToList()[0];
        inventory.Remove(item);

        //inventoryScreen.UpdateInventoryScreen(inventory);
    }
}
