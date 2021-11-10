using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
	public Sprite[] backgroundSprites;
	private int currentBackground = 0;
	private SpriteRenderer background;

	private void Start()
	{
		background = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
	}

	public void UpdateBackground()
	{
		Debug.Log("UPDATING BACKGROUND");
		background.sprite = backgroundSprites[++currentBackground];
	}
}
