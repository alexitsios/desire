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
		switch(scene)
		{
            case SceneName.Stern:
                debugText.text = "- Talking to the Vacuum Robot a second time will still trigger the whole conversation. I'll fix it later\n- You can kinda see the edge of the background if you go all the way to the right. I'll see what's causing this";
                break;

            case SceneName.Funnel:
                debugText.text = "- I'm aware the cursor is not changing when hovering over some objects. I'm not sure what's going on, but I'm investigating it already";
                break;

            case SceneName.Superstructure_out:
                debugText.text = "- This background cannot become any smaller without making the camera go off bounds";
                break;

            case SceneName.Superstructure_in:
                debugText.text = "- I was thinking of adding a mini-game, or something more interactable, before Led can open the door to the generator room";
                break;

            case SceneName.Generator_room:
                debugText.text = "- I was thinking of adding a mini-game, or something more interactable, before Led can fix the lights\n- THE LOGS ARE WORKING, BUT THE GAME CAN SOFTLOCK IF YOU TRY TO READ THEM. THE CLICK DETECTION IS WONKY, AND I'M WORKING TO FIX IT";
                break;
            
            case SceneName.Bridge:
                debugText.text = "- This background cannot become any smaller without making the camera go off bounds\n-The script could use some work to better reflect what's happening on-screen\n- I'll probably cut the chair from the background to use it as an object, to avoid having to block all movement through it";
                break;
		}
	}
}
