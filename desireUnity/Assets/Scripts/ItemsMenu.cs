using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsMenu : MonoBehaviour
{
	public PlayerInteraction PlayerInteraction { get; set; }

	private Animator animator;
    [SerializeField] private GameObject[] inventorySlots;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        for(int i = 0; i < inventorySlots.Length; i++)
		{
            var slotNumber = i;
            var btn = inventorySlots[slotNumber].GetComponent<Button>();

            btn.onClick.AddListener(() => {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>().UseItem(slotNumber);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetCursorAction(CursorAction.ItemSelected);
            });
		}

        inventorySlots[0].transform.parent.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    /// <summary>
    ///     Changes the slots on the Inventory UI to reflect the player's inventory
    /// </summary>
    public void UpdateInventoryScreen()
	{
        //I kind of want to just cache this as GameManager.instance.inventoryManager...
        var inventory = GameManager.instance.gameObject.GetComponent<InventoryManager>().Inventory;
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

    public void OpenSettings()
	{
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OpenSettings();
	}
}
