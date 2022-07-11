using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour
{
    static GameObject debugArea;
    static TMP_Text debugText;

    void Awake()
    {
        debugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        debugArea = GameObject.Find("DebugArea");
        debugArea.SetActive(false);

        GetComponent<Button>().onClick.AddListener(() =>
        {
            debugArea.SetActive(!debugArea.activeSelf);
        });
    }

    public void SetDebugText(SceneName scene)
	{
		debugText.text = scene switch
		{
			SceneName.Superstructure_out => "- This background cannot become any smaller without making the camera go off bounds",
			SceneName.Superstructure_in => "- I was thinking of adding a mini-game, or something more interactable, before Led can open the door to the generator room",
			SceneName.Generator_room => "- I was thinking of adding a mini-game, or something more interactable, before Led can fix the lights",
			SceneName.Bridge => "- This background cannot become any smaller without making the camera go off bounds\n-The script could use some work to better reflect what's happening on-screen\n- I'll probably cut the chair from the background to use it as an object, to avoid having to block all movement through it",
			_ => "",
		};
	}
}
