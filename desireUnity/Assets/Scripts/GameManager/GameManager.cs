using Fungus;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static GameManager instance;
    public GameObject clickIndicator;
    public CursorAnimations cursor;
    public InventoryItem[] itemList;
    public bool CanCursorChange { get; set; } = true;

    private PlayerInteraction playerInteraction;
    private bool isPlaying = false;
    private CursorAction currentAction = CursorAction.Pointer;
    private Flowchart flowchart;
    private TextMeshProUGUI interactDialog;
    private Coroutine cursorWaitCoroutine;

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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "00_StartGame" && scene.name != "UI_Inventory")
		{
            playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

            // Loads the Inventory UI if it's not already loaded
            if(!SceneManager.GetSceneByBuildIndex(1).isLoaded)
                SceneManager.LoadScene(1, LoadSceneMode.Additive);

            flowchart = GameObject.Find("CutscenesFlowchart").GetComponent<Flowchart>();
            GetComponent<InkManager>().StartInkManager();
            GetComponent<CanvasManager>().StartCanvasManager();
            interactDialog = GameObject.FindGameObjectWithTag("InteractDialog").GetComponent<TextMeshProUGUI>();

            Cursor.SetCursor(cursor.mousePointerToQuestion[0], Vector2.zero, CursorMode.Auto);
            SetInteractDialogActive(false);
            isPlaying = true;

            GetComponent<CanvasManager>().LoadLastBackground((SceneName) scene.buildIndex);
            flowchart.ExecuteBlock("OnLoad");
        } 
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
            Vector3 dialogPosition = new Vector3(Input.mousePosition.x + 40, Input.mousePosition.y - 50, 0);
            interactDialog.transform.position = dialogPosition;
        }
    }

    //SceneStates
    public void StartGame()
    {
        // Loads the first scene
        SceneManager.LoadScene("01_Stern");

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
        SetCursorAction(CursorAction.Pointer);
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

    public void SpawnPlayerAtPoint(int spawnIndex)
	{
        var player = GameObject.FindGameObjectWithTag("Player");
        var spawnPoint = GameObject.Find($"Spawn{spawnIndex}");

        player.transform.position = spawnPoint.transform.position;
	}
}
