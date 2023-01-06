using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [Space]
    [SerializeField] private GameObject aboutUi;
    [SerializeField] private Button aboutButton, closeAbout;
    [Space]
    [SerializeField] private Button optionsButton;
    [Space]
    [SerializeField] private Button exitButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);

        aboutButton.onClick.AddListener(delegate { AboutUi(true); });
        closeAbout.onClick.AddListener(delegate { AboutUi(false); });

        optionsButton.onClick.AddListener(OpenSettings);

        exitButton.onClick.AddListener(QuitGame);

        PlayerPrefs.SetInt("BeepSound", 1);
        PlayerPrefs.SetInt("Hints", 1);
    }
    private void StartGame()
    {
		// Loads the opening scene
		SceneManager.LoadScene(8, LoadSceneMode.Single);
    }

    //Open/Close the About section on the main menu
    private void AboutUi(bool open)
    {
        aboutUi.gameObject.SetActive(open);
    }

    //Open/Close the SettingsPanel
    private void OpenSettings()
    {
        GameManager.instance.OpenSettings();
    }

    private void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
