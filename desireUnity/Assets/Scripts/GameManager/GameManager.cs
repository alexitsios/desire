using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject clickIndicator;

    private PlayerInteraction playerInteraction;
    private bool isPlaying = false;
    private CursorAction currentAction = CursorAction.Pointer;

    public Texture2D[] mousePointerToQuestion;
    public Texture2D[] mousePointerToRightArrow;
    public Texture2D[] mousePointerToLeftArrow;
    //public Texture2D[] mousePointerToWait;

    //private GameObject itemsMenu;

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
        Debug.Log("Scene loaded");

        if(scene.name != "00_StartGame")
		{
            playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();

            // Loads the Inventory UI if it's not already loaded
            if(!SceneManager.GetSceneByBuildIndex(0).isLoaded)
                SceneManager.LoadScene(0, LoadSceneMode.Additive);
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
        // Loads the first scene, and adds the inventory to it (scene 0 is UI_Inventory)
        SceneManager.LoadScene("01_Stern");

        isPlaying = true;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("00_StartGame");
    }
    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("LoadState");
    }

    public void SetCursorAction(CursorAction newAction)
	{
        StartCoroutine(AnimateCursorTransition(currentAction, newAction));
	}

    private IEnumerator AnimateCursorTransition(CursorAction currentAction, CursorAction newAction)
	{
        Texture2D[] textureArray = new Texture2D[1];
        bool isAnimationReversed;

        isAnimationReversed = (newAction == CursorAction.Pointer);

        switch((isAnimationReversed) ? currentAction : newAction)
		{
            case CursorAction.LeftArrow:
                textureArray = mousePointerToLeftArrow;
                break;

            case CursorAction.RightArrow:
                textureArray = mousePointerToRightArrow;
                break;

            case CursorAction.Question:
                textureArray = mousePointerToQuestion;
                break;

            case CursorAction.Wait:
                break;
		}

        for(int i = 0; i < textureArray.Length; i++)
		{
            var j = (isAnimationReversed ? (textureArray.Length - i - 1) : i);

            Cursor.SetCursor(textureArray[j], Vector2.zero, CursorMode.Auto);
            yield return new WaitForSeconds(0.05f);
        }

        this.currentAction = newAction;
	}
}
