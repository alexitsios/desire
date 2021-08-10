using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerMovement player;
    private GameObject[] allNPC;

    public bool inConversation;

    private Flowchart flowchart;

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

    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("LoadState");
    }

    public void StartConversation(string NPC)
    {
        inConversation = true;
        flowchart.ExecuteBlock(NPC);
        Debug.Log("STARTED CONVERSATION WITH" + NPC);
    }

    public void FinishConversation()
    {
        inConversation = false;
        Debug.Log("FINISHED CONVERSATION");
    }

}
