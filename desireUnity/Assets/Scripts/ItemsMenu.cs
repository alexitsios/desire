using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsMenu : MonoBehaviour
{
	public PlayerInteraction PlayerInteraction { get; set; }

	private Animator animator;
    private GameObject[] inventorySlots = new GameObject[6];

	void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        for(var i = 1; i <= 6; i++)
        {
            inventorySlots[i - 1] = GameObject.Find($"Item0{i}");
        }

        for(int i = 0; i < inventorySlots.Length; i++)
		{
            var slotNumber = i;
            var btn = inventorySlots[slotNumber].GetComponent<Button>();

            btn.onClick.AddListener(() => {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>().UseItem(slotNumber);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetCursorAction(CursorAction.ItemSelected);
            });
		}

        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    /// <summary>
    ///     Changes the slots on the Inventory UI to reflect the player's inventory
    /// </summary>
    public void UpdateInventoryScreen(List<InventoryItem> inventory)
	{
        var slots = GameObject.FindGameObjectsWithTag("ItemSlot");

        for(int i = 0; i < slots.Length; i++)
		{
            // If the current inventory slot contains an item, change the UI slot
            if(i < inventory.Count)
			{
                slots[i].GetComponent<Image>().enabled = true;
                slots[i].GetComponent<Image>().sprite = inventory[i]._sprite;
            }
            // Otherwise, disable the UI slot
            else
			{
                slots[i].GetComponent<Image>().enabled = false;
            }
        }
	}

    public void OpenSettings()
	{
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OpenSettings();
	}
}
