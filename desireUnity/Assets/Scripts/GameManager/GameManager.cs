using Fungus;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // PROPERTIES
    public bool CanCursorChange { get; set; } = true;
    public Settings Settings { get => _settings; set => UpdateSettings(value); }
    //

    public static GameManager instance;
    public GameObject clickIndicator;
    public CursorAnimations cursor;
    public InventoryItem[] itemList;
    public AudioEffects[] SFX;
    public GameObject inventoryUI;
    public GameObject mainUI;
    public GameObject settingsUI;
    public GameObject dataPadUI;
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
    private GameObject _dataPadUiInstance;
    private TMP_Text _shipWarnings;
    private Settings _settings;
    private float warningWaitTime;
    private bool warningActive;
    private bool gameLoaded = false;
    private WriterAudio wa;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        PlayerPrefs.DeleteAll();
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);

        _ink = GetComponent<InkManager>();
        _translationManager = GetComponent<TranslationManager>();

        Settings = new Settings()
        {
            Language = Language.English,
            BeepSound = true,
            BGVolume = 1,
            FXVolume = 1,
            MasterVolume = 1
        };
    }

    private IEnumerator StartScene(Scene scene, bool fadeIn)
	{
        while(_inventoryInstance == null || _mainUiInstance == null)
            yield return null;

        GetComponent<CanvasManager>().LoadLastBackground((SceneName)scene.buildIndex);

		if(fadeIn)
            yield return GetComponent<CanvasManager>().Fade("in", 1);

        flowchart.ExecuteBlock("OnLoad");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 8)
            return;

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
            _shipWarnings = GameObject.Find("ShipWarning").GetComponent<TMP_Text>();
            wa = GameObject.FindGameObjectWithTag("SayDialog").GetComponent<WriterAudio>();
            _shipWarnings.CrossFadeAlpha(0, 0, false);

            _ink.StartInkManager();
            interactDialog = GameObject.FindGameObjectWithTag("InteractDialog").GetComponent<TextMeshProUGUI>();
            SetInteractDialogActive(false);

            DontDestroyOnLoad(_mainUiInstance);

            if(!Settings.BeepSound)
			{
                wa.volume = 0f;
			}
        }

        if(_dataPadUiInstance == null && scene.buildIndex != 0)
		{
            _dataPadUiInstance = Instantiate(dataPadUI);
            DontDestroyOnLoad(_dataPadUiInstance);
            _dataPadUiInstance.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 0);
        }

        if(scene.name == "00_StartGame")
		{
            gameLoaded = _ink.LoadGame();

            if(!gameLoaded)
			{
                // Removes the Load button from the main menu if no save game is available
                Destroy(GameObject.Find("Load"));
			}
		}
        else if(scene.name == "07_EndGame")
		{
            Destroy(_mainUiInstance);
            Destroy(_inventoryInstance);
            Destroy(_dataPadUiInstance);
		}
        else if(scene.name != "00_StartGame" && scene.name != "07_EndGame")
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

            var canFadeIn = true;

            // Changes the scene depending on what the player already did
            switch ((SceneName) scene.buildIndex)
			{
                case SceneName.Stern:
                    // Blocks movement if this is the first time the player visits the Stern (i.e. if the game just started)
                    if (!_ink.GetVariable<bool>("funnel_visited"))
					{
                        canFadeIn = false;
                        player.GetComponent<PlayerMovement>().IsTrapped = true;
                        player.GetComponent<PlayerMovement>().AcquiredArm = false;
                        player.GetComponent<Animator>().speed = 0;
                        player.transform.GetChild(0).transform.localPosition = new Vector3(-0.08f, -0.15f, 0);
                    }

                    // Removes the All-in-One Tool
                    if (_ink.GetVariable<bool>("acquired_tool"))
                        Destroy(GameObject.Find("AllInOneTool"));

                    // Moves the Vacuum Robot
                    if(_ink.GetVariable<bool>("acquired_leg"))
                        GameObject.Find("VacuumRobot").transform.position = GameObject.Find("TrashBin").transform.position;

                    break;

                case SceneName.Funnel:
                    SetPlayerAndShadowSize(0.6f);
                    break;

                case SceneName.Superstructure_out:
                    SetPlayerAndShadowSize(0.8f);
                    break;

                case SceneName.Superstructure_in:
                    SetPlayerAndShadowSize(0.6f);
                    break;

                case SceneName.Generator_room:
                    SetPlayerAndShadowSize(0.7f);
                    break;

                case SceneName.Bridge:
                    SetPlayerAndShadowSize(0.8f);
                    break;
            }

			if(Debug.isDebugBuild)
			{
                GameObject.Find("DebugInfo").GetComponent<DebugInfo>().SetDebugText((SceneName) scene.buildIndex);
            }
			else
			{
                Destroy(GameObject.Find("DebugInfo"));
			}

            _ink.CurrentScene = (SceneName) scene.buildIndex;

            StartCoroutine(StartScene(scene, canFadeIn));
        } 
    }

    private void SetPlayerAndShadowSize(float playerHeight)
	{
        player.GetComponent<SpriteRenderer>().size = new Vector2(playerHeight / 2, playerHeight);
        player.transform.GetChild(0).localScale = new Vector3(playerHeight, playerHeight, 0f);
        player.transform.GetChild(0).transform.localPosition = new Vector3(0f, -((playerHeight * 0.3f) - 0.01f), 0);
    }

    public void OpenSettings()
	{
        Instantiate(settingsUI, transform);
	}

    //Save state
    public void SaveState()
    {
        _ink.SaveGame();
    }

    public void LoadGame()
	{
        StartCoroutine(LoadState());
	}

    private IEnumerator LoadState()
    {
        LoadSceneAndSpawnPlayer(_ink.CurrentScene, 0, false);

        if(player == null)
		{
            yield return null;
		}

        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.IsTrapped = _ink.GetVariable<bool>("acquired_leg");
        playerMovement.AcquiredArm = _ink.GetVariable<bool>("acquired_arm");
    }

    private void Update()
    {
        if(isPlaying)
		{
            var textWidth = interactDialog.GetRenderedValues(true).x;
            var posX = Mathf.Clamp(Input.mousePosition.x + 40, textWidth / 2, Screen.width - (textWidth / 2));

            Vector3 dialogPosition = new Vector3(posX, Input.mousePosition.y - 50, 0);
            interactDialog.transform.position = dialogPosition;
        }

        if(warningActive && Input.GetMouseButtonDown(0))
		{
            warningWaitTime = 0f;
		}
    }

    //SceneStates
    public void StartGame()
    {
		// Loads the opening scene
		SceneManager.LoadScene(8, LoadSceneMode.Single);
	}
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("00_StartGame");
    }

    public void QuitGame()
	{
        Application.Quit();
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

    public IEnumerator LoadSceneAndSpawnPlayer(SceneName scene, int spawnIndex, bool fadeScreen = true)
	{
		if(fadeScreen)
		{
            yield return GetComponent<CanvasManager>().Fade("out", 1);
		}

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

    public void UpdateSettings(Settings newSettings)
	{
        _settings = newSettings;

        var fxVolume = Mathf.Lerp(0f, 1f, (_settings.FXVolume + _settings.MasterVolume) / 2);
        GetComponent<AudioSource>().volume = fxVolume;

        if(!Settings.BeepSound)
        {
            wa.volume = 0f;
        }

        foreach(var prop in FindObjectsOfType<PropBase>())
        {
            prop.DisplayHints = Settings.ShowHints;
        }
    }

    public IEnumerator DisplayMessage(string messageId, string messageType)
	{
        _shipWarnings.color = messageType == "warning" ? Color.red : Color.white;
        _shipWarnings.text = _translationManager.GetTranslatedLine(messageId);
        _shipWarnings.CrossFadeAlpha(1, 0.5f, false);

        warningWaitTime = _shipWarnings.text.Length / 10;

        while(warningWaitTime > 0)
		{
            warningActive = true;
            warningWaitTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
		}

        _shipWarnings.CrossFadeAlpha(0, 0.5f, false);

        yield return new WaitForSeconds(1f);
        warningActive = false;
    }

    public IEnumerator SetDataPadVisibility(bool active)
	{
        var sprite = _dataPadUiInstance.GetComponentInChildren<Image>();

		if(active)
		{
            for(float i = 0; i <= 1; i += Time.deltaTime / 0.5f)
            {
                sprite.color = new Color(1f, 1f, 1f, i); ;
                yield return null;
            }

            sprite.color = new Color(1f, 1f, 1f, 1);
        }
        else
		{
            for(float i = 1; i >= 0; i -= Time.deltaTime / 0.5f)
            {
                sprite.color = new Color(1f, 1f, 1f, i); ;
                yield return null;
            }

            sprite.color = new Color(1f, 1f, 1f, 0);
        }
	}
}
