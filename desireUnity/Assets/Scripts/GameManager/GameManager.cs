using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct CursorAnimations
	{
        public Texture2D[] mousePointerToQuestion;
        public Texture2D[] mousePointerToRightArrow;
        public Texture2D[] mousePointerToLeftArrow;
        public Texture2D[] mousePointerToDialog;
    }

    public static GameManager instance;
    public GameObject clickIndicator;

    private PlayerInteraction playerInteraction;
    private bool isPlaying = false;
    private CursorAction currentAction = CursorAction.Pointer;
    private Flowchart flowchart;

    public CursorAnimations cursor;
    public InventoryItem[] itemList;

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

        //Cursor.SetCursor(mousePoint, Vector2.zero, CursorMode.Auto);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "00_StartGame")
		{
            playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

            // Loads the Inventory UI if it's not already loaded
            if(!SceneManager.GetSceneByBuildIndex(0).isLoaded)
                SceneManager.LoadScene(0, LoadSceneMode.Additive);

            flowchart = GameObject.Find("CutscenesFlowchart").GetComponent<Flowchart>();
            GetComponent<InkManager>().StartInkManager();

            switch(scene.buildIndex)
			{
                case (int) SceneName.Stern:
                    flowchart.ExecuteBlock("Opening");
                    break;

                case (int) SceneName.Funnel:
                    break;
			}
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
            if(Input.GetMouseButtonDown(0) && !playerInteraction.isInteracting)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //clickIndicator.transform.position = mousePosition;
                //clickIndicator.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    //SceneStates
    public void StartGame()
    {
        // Loads the first scene
        SceneManager.LoadScene("01_Stern");

        isPlaying = true;
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
        StartCoroutine(AnimateCursorTransition(currentAction, newAction));
	}

    /// <summary>
    ///     Animates the cursor from the current action to the desired action
    /// </summary>
    private IEnumerator AnimateCursorTransition(CursorAction currentAction, CursorAction newAction)
	{
        Texture2D[] textureArray = new Texture2D[1];
        bool isAnimationReversed;

        // If the animation is going from <Action> to Pointer, then play the animation from Pointer to <Action>, but reversed
        isAnimationReversed = (newAction == CursorAction.Pointer);

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

            case CursorAction.Wait:
                break;
		}

        this.currentAction = newAction;

        // If the animation is not reversed, applies the frames from the animation array going from 0 to (size - 1)
        // If the animation IS reversed, applies the frames going from (size - 1) to 0
        for(int i = 0; i < textureArray.Length; i++)
		{
            var j = (isAnimationReversed ? (textureArray.Length - i - 1) : i);

            Cursor.SetCursor(textureArray[j], Vector2.zero, CursorMode.Auto);
            yield return new WaitForSeconds(0.05f);
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
}
