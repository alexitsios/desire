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
    }

    public void UseItem(GameObject item)
    {
        //gameManager.UsedItem(item.name);
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

    public void UpdateInventoryScreen(List<ItemBase> inventory)
	{
        for(int i = 0; i < inventorySlots.Length; i++)
		{
            inventorySlots[i].GetComponent<Image>().sprite = inventory[i].itemImage ?? null;
        }
	}
}
