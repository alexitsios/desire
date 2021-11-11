using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject clickIndicator;

    private PlayerInteraction playerInteraction;
    private bool isPlaying = false;

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
}
