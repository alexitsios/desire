using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject clickIndicator;

    private PlayerInteraction playerInteraction;

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
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
    }

    //Save state
    public void SaveState()
    {
        Debug.Log("SaveState");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerInteraction.isInteracting)
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
}
