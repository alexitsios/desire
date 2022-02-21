using Fungus;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct CursorAnimations
	{
        public Texture2D[] mousePointerToQuestion;
        public Texture2D[] mousePointerToRightArrow;
        public Texture2D[] mousePointerToLeftArrow;
        public Texture2D[] mousePointerToDialog;
        public Texture2D[] mousePointerToTarget;
        public Texture2D[] mouseWait;
    }

    [Serializable]
    public struct AudioEffects
	{
        public string sfxName;
        public AudioClip sfx;
	}

    public static GameManager instance;
    public GameObject clickIndicator;
    public CursorAnimations cursor;
    public InventoryItem[] itemList;
    public AudioEffects[] SFX;
	public bool CanCursorChange { get; set; } = true;
    public GameObject inventoryUI;
    public GameObject mainUI;
    public GameObject player;

    private PlayerInteraction playerInteraction;
    private bool isPlaying = false;
    private CursorAction currentAction = CursorAction.Pointer;
    private Flowchart flowchart;
    private TextMeshProUGUI interactDialog;
    private Coroutine cursorWaitCoroutine;
    private InkManager _ink;
    private int playerSpawn = 1;
    private TranslationManager _translationManager;
    private GameObject _inventoryInstance;
    private GameObject _mainUiInstance;

    // DEBUG
    private float _cameraSize;
    public float CameraSize { get => _cameraSize; set { _cameraSize = value; UpdateInfoText(); } }
    private Vector2 _ledSize;
    public Vector2 LedSize { get => _ledSize; set { _ledSize = value; UpdateInfoText(); } }

    private void UpdateInfoText()
	{
        GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"Camera Size: {CameraSize}\nLed Size: {{x: {LedSize.x}; y: {LedSize.y}}}";
	}

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        PlayerPrefs.DeleteAll();
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);

        _ink = GetComponent<InkManager>();
        _translationManager = GetComponent<TranslationManager>();
    }

    private IEnumerator StartScene(Scene scene)
	{
        while(_inventoryInstance == null || _mainUiInstance == null)
            yield return null;

        GetComponent<CanvasManager>().LoadLastBackground((SceneName)scene.buildIndex);
        flowchart.ExecuteBlock("OnLoad");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(_inventoryInstance == null && scene.buildIndex != 0)
		{
            _inventoryInstance = Instantiate(inventoryUI);

            var inventory = GetComponent<InventoryManager>().Inventory;
            GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>().UpdateInventoryScreen(inventory);

            DontDestroyOnLoad(_inventoryInstance);
        }
        
        if(_mainUiInstance == null && scene.buildIndex != 0)
		{
            _mainUiInstance = Instantiate(mainUI);

            _ink.StartInkManager();
            interactDialog = GameObject.FindGameObjectWithTag("InteractDialog").GetComponent<TextMeshProUGUI>();
            SetInteractDialogActive(false);

            DontDestroyOnLoad(_mainUiInstance);
        }

        if(scene.name == "07_EndGame")
		{
            Destroy(_mainUiInstance);
            Destroy(_inventoryInstance);
		}

        if(scene.name != "00_StartGame" && scene.name != "07_EndGame")
		{
            GetComponent<InventoryManager>().StartInventoryManager();

            GetComponent<TranslationManager>().LoadTranslation(Language.English, (SceneName) scene.buildIndex);
            playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

            _inventoryInstance.GetComponentInChildren<ItemsMenu>().PlayerInteraction = playerInteraction;

            flowchart = GameObject.Find("CutscenesFlowchart").GetComponent<Flowchart>();
            GetComponent<CanvasManager>().StartCanvasManager();

            Cursor.SetCursor(cursor.mousePointerToQuestion[0], Vector2.zero, CursorMode.Auto);
            isPlaying = true;

            player = GameObject.FindGameObjectWithTag("Player");

            var spawnPoint = GameObject.Find($"Spawn{playerSpawn}");
            player.transform.position = spawnPoint.transform.position;
            _ink.Interaction = player.GetComponent<PlayerInteraction>();

            player.GetComponent<PlayerMovement>().IsTrapped = false;
            player.GetComponent<PlayerMovement>().AcquiredArm = true;

            // Changes the scene depending on what the player already did
            switch ((SceneName) scene.buildIndex)
			{
                case SceneName.Stern:
                    // Blocks movement if this is the first time the player visits the Stern (i.e. if the game just started)
                    if (!_ink.GetVariable<bool>("funnel_visited"))
					{
                        player.GetComponent<PlayerMovement>().IsTrapped = true;
                        player.GetComponent<PlayerMovement>().AcquiredArm = false;
					}

                    // Removes the All-in-One Tool
                    if (_ink.GetVariable<bool>("acquired_tool"))
                        Destroy(GameObject.Find("AllInOneTool"));

                    // Moves the Vacuum Robot
                    if(_ink.GetVariable<bool>("acquired_leg"))
                        GameObject.Find("VacuumRobot").transform.position = GameObject.Find("TrashBin").transform.position;

                    break;

                case SceneName.Funnel:
                    break;
			}

            StartCoroutine(StartScene(scene));
        } 
    }

    public void OpenSettings()
	{
        SceneManager.LoadScene("UI_SettingsMenu", LoadSceneMode.Additive);
	}

    //Save state
    public void SaveState()
    {
        Debug.Log("SaveState");
    }

    private void Update()
    {
        if(isPlaying)
		{
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            var textWidth = interactDialog.GetRenderedValues(true).x;
            var posX = Mathf.Clamp(Input.mousePosition.x + 40, textWidth / 2, Screen.width - (textWidth / 2));

            Vector3 dialogPosition = new Vector3(posX, Input.mousePosition.y - 50, 0);
            interactDialog.transform.position = dialogPosition;
        }
    }

    //SceneStates
    public void StartGame()
    {
        // Loads the first scene
        LoadSceneAndSpawnPlayer(SceneName.Stern, 1);

    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("00_StartGame");
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("LoadState");
    }

    public void SetCursorAction(CursorAction newAction)
	{
        if(newAction == CursorAction.Wait)
		{
            cursorWaitCoroutine = StartCoroutine(SetCursorToWait());
            CanCursorChange = false;
		}
        else
            StartCoroutine(AnimateCursorTransition(currentAction, newAction));
    }

    public IEnumerator SetCursorToWait()
	{
        var textureArray = cursor.mouseWait;
        int i = 0;

        // Continuously animate the Wait cursor. This coroutine only stops when explicitly told to
        while(true)
		{
            Cursor.SetCursor(textureArray[i++ % textureArray.Length], Vector2.zero, CursorMode.Auto);
            yield return new WaitForSeconds(0.1f);
        }
	}

    public void EndCursorWait()
	{
        CanCursorChange = true;
        StopCoroutine(cursorWaitCoroutine);
        Cursor.SetCursor(cursor.mousePointerToRightArrow[0], Vector2.zero, CursorMode.Auto);
	}

    /// <summary>
    ///     Animates the cursor from the current action to the desired action
    /// </summary>
    private IEnumerator AnimateCursorTransition(CursorAction currentAction, CursorAction newAction)
	{
        Texture2D[] textureArray = null;
        bool isAnimationReversed;
        bool isAnimationLooped = false;

        // If the animation is going from <Action> to Pointer, then play the animation from Pointer to <Action>, but reversed
        isAnimationReversed = (newAction == CursorAction.Pointer);

        if(CanCursorChange)
		{
            switch((isAnimationReversed) ? currentAction : newAction)
            {
                case CursorAction.LeftArrow:
                    textureArray = cursor.mousePointerToLeftArrow;
                    break;

                case CursorAction.RightArrow:
                    textureArray = cursor.mousePointerToRightArrow;
                    break;

                case CursorAction.Question:
                    textureArray = cursor.mousePointerToQuestion;
                    break;

                case CursorAction.Dialog:
                    textureArray = cursor.mousePointerToDialog;
                    break;

                case CursorAction.ItemSelected:
                    CanCursorChange = false;
                    textureArray = cursor.mousePointerToTarget;
                    break;
            }

            this.currentAction = newAction;

            if(textureArray != null)
			{
                // If the animation is not reversed, applies the frames from the animation array going from 0 to (size - 1)
                // If the animation IS reversed, applies the frames going from (size - 1) to 0
                for(int i = 0; i < textureArray.Length; i++)
		        {
                    var j = (isAnimationReversed ? (textureArray.Length - i - 1) : i);

                    Cursor.SetCursor(textureArray[j], Vector2.zero, CursorMode.Auto);
                    yield return new WaitForSeconds(0.05f);
			    }
            }

            if(isAnimationLooped)
                SetCursorAction(this.currentAction);
        }

	}

    public InventoryItem GetItemProperties(ItemType type)
	{
        foreach(var item in itemList)
		{
            if(item._type == type)
			{
                return item;
			}
		}

        return new InventoryItem(ItemType.NoItem, null);
	}

    public void SetInteractDialogActive(bool active)
	{
        interactDialog.gameObject.SetActive(active);
	}

    public void SetInteractDialogText(string text)
	{
        string translatedItemName;

        if(playerInteraction._selectedItem != ItemType.NoItem)
            translatedItemName = _translationManager.GetTranslatedItem(playerInteraction._selectedItem.ToString()) + " → ";
        else
            translatedItemName = "";

        SetInteractDialogActive(true);

        interactDialog.text = translatedItemName + text;
	}

    public void LoadSceneAndSpawnPlayer(SceneName scene, int spawnIndex)
	{
        SceneManager.LoadScene((int) scene, LoadSceneMode.Single);
        GetComponent<TranslationManager>().LoadTranslation(Language.English, scene);

        playerSpawn = spawnIndex;
	}

    public AudioClip GetSFXByName(string name)
	{
        foreach(var clip in SFX)
		{
            if(clip.sfxName == name)
                return clip.sfx;
		}

        return null;
	}

    public void StartEndingCutscene()
	{
        isPlaying = false;
        SceneManager.LoadScene(7);
    }
}
