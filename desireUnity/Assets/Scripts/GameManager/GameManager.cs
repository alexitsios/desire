using Fungus;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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

    private PlayerInteraction playerInteraction;
    private bool isPlaying = false;
    private CursorAction currentAction = CursorAction.Pointer;
    private Flowchart flowchart;
    private TextMeshProUGUI interactDialog;
    private Coroutine cursorWaitCoroutine;
    private InkManager _ink;
    private int playerSpawn = 1;
    private MenuBase settingsMenu;

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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sets the UI to show the player's inventory whenever the UI scene finishes loading
        if(scene.buildIndex == (int) SceneName.Inventory)
		{
            var inventory = GetComponent<InventoryManager>().Inventory;
            GameObject.FindGameObjectWithTag("ItemsMenu").GetComponent<ItemsMenu>().UpdateInventoryScreen(inventory);
            GetComponent<InventoryManager>().StartInventoryManager();
        }
        else if(scene.buildIndex == (int) SceneName.Settings)
		{
            settingsMenu = new MenuBase()
            {
				Options = new List<MenuOption>() {
					new MenuOption("Language", MenuOptionType.Dropdown),
					new MenuOption("Graphical Quality", MenuOptionType.Dropdown)
				}
			};

            settingsMenu.RenderMenu();
		}
        else if(scene.name != "00_StartGame")
		{
            playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

            // Loads the Inventory UI if it's not already loaded
            if(!SceneManager.GetSceneByBuildIndex(1).isLoaded)
                SceneManager.LoadScene((int) SceneName.Inventory, LoadSceneMode.Additive);

            flowchart = GameObject.Find("CutscenesFlowchart").GetComponent<Flowchart>();
            GetComponent<InkManager>().StartInkManager();
            GetComponent<CanvasManager>().StartCanvasManager();
            interactDialog = GameObject.FindGameObjectWithTag("InteractDialog").GetComponent<TextMeshProUGUI>();

            Cursor.SetCursor(cursor.mousePointerToQuestion[0], Vector2.zero, CursorMode.Auto);
            SetInteractDialogActive(false);
            isPlaying = true;

            var player = GameObject.FindGameObjectWithTag("Player");
            var spawnPoint = GameObject.Find($"Spawn{playerSpawn}");

            player.transform.position = spawnPoint.transform.position;

            // Changes the scene depending on what the player already did
            switch((SceneName) scene.buildIndex)
			{
                case SceneName.Stern:
                    // Removes the All-in-One Tool
                    if(_ink.GetVariable<bool>("acquired_tool"))
                        Destroy(GameObject.Find("AllInOneTool"));

                    // Moves the Vacuum Robot
                    if(_ink.GetVariable<bool>("acquired_leg"))
                        GameObject.Find("VacuumRobot").transform.position = GameObject.Find("TrashBin").transform.position;

                    break;

                case SceneName.Funnel:
                    break;
			}
            
            GetComponent<CanvasManager>().LoadLastBackground((SceneName) scene.buildIndex);
            flowchart.ExecuteBlock("OnLoad");
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
            var textWidth = interactDialog.GetRenderedValues(true).x;
            var posX = Mathf.Clamp(Input.mousePosition.x + 40, textWidth / 2, Screen.width - (textWidth / 2));

            Vector3 dialogPosition = new Vector3(posX, Input.mousePosition.y - 50, 0);
            interactDialog.transform.position = dialogPosition;
        }
    }

    //SceneStates
    public void StartGame()
    {
        GetComponent<TranslationManager>().LoadTranslation(Language.Brazilian_Portuguese);

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
        SetInteractDialogActive(true);
        interactDialog.text = playerInteraction._selectedItem != ItemType.NoItem ? $"{playerInteraction._selectedItem} → " : "";

        interactDialog.text += text;
	}

    public void LoadSceneAndSpawnPlayer(SceneName scene, int spawnIndex)
	{
        SceneManager.LoadScene((int) scene, LoadSceneMode.Single);

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
}
