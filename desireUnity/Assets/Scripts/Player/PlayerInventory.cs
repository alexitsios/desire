using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> inventory = new List<InventoryItem>();
    private ItemsMenu inventoryScreen;
    private PlayerInteraction _interaction;

	public void Start()
	{
        inventoryScreen = GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>();
        _interaction = GetComponent<PlayerInteraction>();
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

    /// <summary>
    ///     Sets up an item to be used the next time the player interacts with something
    /// </summary>
    public void UseItem(int slotNumber)
    {
        var item = inventory[slotNumber]._type;
        _interaction.SetSelectedItem(item);
    }
}
