using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> Inventory { get; } =  new List<InventoryItem>();
    public GameObject itemObatinedDialog;

    private ItemsMenu inventoryScreen;
    private PlayerInteraction _interaction;

	public void StartInventoryManager()
	{
        inventoryScreen = GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>();
        _interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
	}

	/// <summary>
	///     Adds a new item to the player's inventory
	/// </summary>
	public void AddItem(InventoryItem item)
    {
        Inventory.Add(item);

        inventoryScreen.UpdateInventoryScreen(Inventory);

        var dialog = Instantiate(itemObatinedDialog, transform);
        GameObject.Find("ItemObtainedName").GetComponent<TextMeshProUGUI>().text = $"{item._type}";
        GameObject.Find("ItemObtainedImage").GetComponent<Image>().sprite = item._sprite;
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
