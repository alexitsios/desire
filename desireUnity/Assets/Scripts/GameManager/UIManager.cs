using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public ItemsMenu itemsMenu;
    [Space]
    [SerializeField] private Button menuButton;

    [SerializeField] private RectTransform inventoryPanel, tasksPanel;
    [SerializeField] private Button inventoryButton, tasksButton;


    private Vector2 inventoryHiddenPos = Vector2.zero, inventoryShownPos;
    private Vector2 tasksHiddenPos = Vector2.zero, tasksShownPos;
    private bool inventoryOpen, tasksOpen;
    private float timeToMove = 0.2f;

    private void Start()
    {
        inventoryShownPos = new Vector2(0, inventoryPanel.sizeDelta.y);
        tasksShownPos = new Vector2(0, tasksPanel.sizeDelta.y);

        menuButton.onClick.AddListener(OpenMenu);
        inventoryButton.onClick.AddListener(ToggleInventory);
        tasksButton.onClick.AddListener(ToggleTasks);
    }

    public void OpenMenu()
    {
        Debug.Log("Opening Menu");
    }

    private void ToggleInventory()
    {
        var end = inventoryShownPos;
        if (inventoryOpen) end = inventoryHiddenPos;
        StartCoroutine(LerpPanel(inventoryPanel, inventoryPanel.anchoredPosition, end));
        inventoryOpen = !inventoryOpen;

        if (inventoryOpen)
        {
            itemsMenu.UpdateInventoryScreen();
        }
    }

    private void ToggleTasks()
    {
        var end = tasksShownPos;
        if (tasksOpen) end = tasksHiddenPos;
        StartCoroutine(LerpPanel(tasksPanel, tasksPanel.anchoredPosition, end));
        tasksOpen = !tasksOpen;
    }

    private IEnumerator LerpPanel(RectTransform panel, Vector2 startPos, Vector2 endPos)
    {
        float t = 0;
        while (t < timeToMove)
        {
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, (t / timeToMove));
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panel.anchoredPosition = endPos;
    }
}
