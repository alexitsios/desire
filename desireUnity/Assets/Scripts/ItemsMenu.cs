using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public PlayerInteraction PlayerInteraction { get; set; }

	private Animator animator;
    private GameObject[] inventorySlots;

	void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        inventorySlots = GameObject.FindGameObjectsWithTag("ItemSlot");

        for(int i = 0; i < inventorySlots.Length; i++)
		{
            var slotNumber = i;
            var btn = inventorySlots[slotNumber].GetComponent<Button>();

            btn.onClick.AddListener(() => {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>().UseItem(slotNumber);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetCursorAction(CursorAction.ItemSelected);
            });
		}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!animator.GetBool("isDown") && !PlayerInteraction.isInteracting)
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
}
