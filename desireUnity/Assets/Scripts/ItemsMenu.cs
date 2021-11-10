using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private GameManager gameManager;
    private GameObject[] inventorySlots;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inventorySlots = GameObject.FindGameObjectsWithTag("ItemSlot");
    }

    public void UseItem(GameObject item)
    {
        gameManager.UsedItem(item.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!animator.GetBool("isDown") && !gameManager.inConversation)
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
