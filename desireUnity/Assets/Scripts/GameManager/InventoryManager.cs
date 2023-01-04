using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	public List<InventoryItem> Inventory { get; } = new List<InventoryItem>();
	public GameObject itemObatinedDialog;

	private ItemsMenu inventoryScreen;
	private PlayerInteraction _interaction;
	private GameManager gameManager;

	public void StartInventoryManager()
	{
		inventoryScreen = UIManager.instance.itemsMenu;
		//inventoryScreen = GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>();
		_interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	/// <summary>
	///     Adds a new item to the player's inventory
	/// </summary>
	public void AddItem(InventoryItem item)
	{
		Inventory.Add(item);

		if(inventoryScreen == null)
		{
			inventoryScreen = UIManager.instance.itemsMenu;
		}

		inventoryScreen.UpdateInventoryScreen();

		var dialog = Instantiate(itemObatinedDialog, transform);

		dialog.GetComponent<ItemObtainedPopup>().SetDialog(item);

		Destroy(dialog, 2f);
	}

	/// <summary>
	///     Removes an item from the player's inventory
	/// </summary>
	public void RemoveItem(ItemType itemType)
	{
		foreach(var item in Inventory)
		{
			if(item._type == itemType)
			{
				Inventory.Remove(item);
				break;
			}
		}

		inventoryScreen.UpdateInventoryScreen();
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
