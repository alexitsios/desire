using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> Inventory { get; } =  new List<InventoryItem>();

    private ItemsMenu inventoryScreen;
    private PlayerInteraction _interaction;

	public void Start()
	{
        inventoryScreen = GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>();
        _interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
	}

	/// <summary>
	///     Adds a new item to the player's inventory
	/// </summary>
	public void AddItem(InventoryItem item)
    {
        Debug.Log("ADDING ITEM " + item._type.ToString());
        Inventory.Add(item);

        inventoryScreen.UpdateInventoryScreen(Inventory);
    }

    /// <summary>
    ///     Removes an item from the player's inventory
    /// </summary>
    public void RemoveItem(ItemType itemType)
    {
        Debug.Log("REMOVING ITEM " + itemType.ToString());
        foreach(var item in Inventory)
		{
            if(item._type == itemType)
			{
                Inventory.Remove(item);
                break;
            }
		}

        inventoryScreen.UpdateInventoryScreen(Inventory);
    }

    /// <summary>
    ///     Sets up an item to be used the next time the player interacts with something
    /// </summary>
    public void UseItem(int slotNumber)
    {
        var item = Inventory[slotNumber]._type;
        _interaction.SetSelectedItem(item);
    }
}
