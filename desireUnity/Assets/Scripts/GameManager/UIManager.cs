using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public TMP_Text shipWarningText;
    [Space]
    [SerializeField] private Button menuButton;
    [SerializeField] private Button inventoryButton, tasksButton;
    [Space]
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private RectTransform tasksPanel;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject aboutUi;
    [SerializeField] private Image portrait;
    [SerializeField] private Sprite[] characterPortrais;
    [SerializeField] private Sprite[] menuBackgrounds;
    [Space]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button aboutButton, optionsButton, exitButton, closeAboutButton;
    private Image pauseMenuBackground;

    private Vector2 inventoryHiddenPos = Vector2.zero, inventoryShownPos;
    private Vector2 tasksHiddenPos = Vector2.zero, tasksShownPos;
    private bool inventoryOpen, tasksOpen;
    private float timeToMove = 0.2f;

    private void Start()
    {
        inventoryShownPos = new Vector2(0, 300);
        tasksShownPos = new Vector2(0, 300);
        pauseMenuBackground = pauseMenu.GetComponent<Image>();

        ButtonSettings();
    }

    private void ButtonSettings()
    {
        menuButton.onClick.AddListener(OpenMenu);
        aboutButton.onClick.AddListener(delegate { AboutUi(true); });
        closeAboutButton.onClick.AddListener(delegate { AboutUi(false); });
        optionsButton.onClick.AddListener(OpenSettings);

        inventoryButton.onClick.AddListener(ToggleInventory);
        tasksButton.onClick.AddListener(ToggleTasks);

        continueButton.onClick.AddListener(delegate 
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false); 
        });

        exitButton.onClick.AddListener(delegate { GameManager.instance.GoToMainMenu(); });
    }

    public void OpenMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

        //Set the background dependent on the current scene
        int i = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(i);
        pauseMenuBackground.sprite = menuBackgrounds[i];
        portrait.sprite = characterPortrais[i];
    }

    //Open/Close the About section on the main menu
    private void AboutUi(bool open)
    {
        aboutUi.gameObject.SetActive(open);
    }

    //Open/Close the SettingsPanel
    private void OpenSettings()
    {
        GameManager.instance.OpenSettings();
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
