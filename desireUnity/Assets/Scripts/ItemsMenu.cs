using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private GameObject[] inventorySlots;
    private PlayerInteraction playerInteraction;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        inventorySlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

        for(int i = 0; i < inventorySlots.Length; i++)
		{
            var slotNumber = i;
            var btn = inventorySlots[slotNumber].GetComponent<Button>();

            btn.onClick.AddListener(() => {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInventory>().UseItem(slotNumber);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetCursorAction(CursorAction.ItemSelected);
            });
		}

        // Updates the inventory screen when the game starts, disabling all the icons
        UpdateInventoryScreen(new List<InventoryItem>());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!animator.GetBool("isDown") && !playerInteraction.isInteracting)
        {
            animator.SetBool("isDown", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator.GetBool("isDown"))
        {
            animator.SetBool("isDown", false);
        }
    }

    /// <summary>
    ///     Changes the slots on the Inventory UI to reflect the player's inventory
    /// </summary>
    public void UpdateInventoryScreen(List<InventoryItem> inventory)
	{
        for(int i = 0; i < inventorySlots.Length; i++)
		{
            // If the current inventory slot contains an item, change the UI slot
            if(i < inventory.Count)
			{
                inventorySlots[i].GetComponent<Image>().enabled = true;
                inventorySlots[i].GetComponent<Image>().sprite = inventory[i]._sprite;
            }
            // Otherwise, disable the UI slot
            else
			{
                inventorySlots[i].GetComponent<Image>().enabled = false;
            }
        }
	}
}
