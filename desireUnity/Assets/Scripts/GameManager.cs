using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerMovement player;
    private GameObject[] allNPC;
    public bool inConversation;
    public GameObject clickIndicator;
    private Flowchart flowchart;

    //private GameObject itemsMenu;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        PlayerPrefs.DeleteAll();
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        allNPC = GameObject.FindGameObjectsWithTag("NPC");
        flowchart = GameObject.FindGameObjectsWithTag("Flowchart")[0].GetComponent<Flowchart>();
        //itemsMenu = GameObject.FindGameObjectsWithTag("ItemsMenu")[0];
        inConversation = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded");
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        allNPC = GameObject.FindGameObjectsWithTag("NPC");
        flowchart = GameObject.FindGameObjectsWithTag("Flowchart")[0].GetComponent<Flowchart>();
    }

    //Save state
    public void SaveState()
    {
        Debug.Log("SaveState");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inConversation)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickIndicator.transform.position = mousePosition;
            clickIndicator.GetComponent<ParticleSystem>().Play();
        }
    }

    //SceneStates
    public void StartGame()
    {
        SceneManager.LoadScene("01_Stern");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("00_StartGame");
    }
    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("LoadState");
    }



    //GamePlay
    public void StartConversation(string NPC)
    {
        inConversation = true;
        flowchart.ExecuteBlock(NPC);
        Debug.Log("STARTED CONVERSATION WITH " + NPC);
    }

    public void FinishConversation()
    {
        inConversation = false;
        Debug.Log("FINISHED CONVERSATION");
    }

    public void UsedItem(string itemName)
    {
        Debug.Log("USED ITEM " + itemName);
    }

}
