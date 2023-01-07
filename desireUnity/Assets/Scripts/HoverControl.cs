using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverControl : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{
    private enum TabType
    {
        Inventory,
        Tasks
    }
    [SerializeField] private UIManager manager;
    [SerializeField] TabType tabType;


    public void OnPointerMove(PointerEventData eventData)
    {
        if (tabType == TabType.Inventory)
            {
                manager.inventoryOpen = true;
                manager.itemsMenu.UpdateInventoryScreen();
            }
        else if (tabType == TabType.Tasks)
            manager.tasksOpen = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tabType == TabType.Inventory)
            manager.inventoryOpen = false;
        else if (tabType == TabType.Tasks)
            manager.tasksOpen = false;
    }
}
