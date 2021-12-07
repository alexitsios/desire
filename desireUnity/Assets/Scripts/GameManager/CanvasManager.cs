using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	public Sprite[] backgroundSprites;

	private int currentBackground = 0;
	private SpriteRenderer _background;
	private Image _fadeImage;

	private void Start()
	{
		_background = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
	}

	public void UpdateBackground()
	{
		_background.sprite = backgroundSprites[++currentBackground];
	}

	public IEnumerator Fade(int fadeType, float fadeTime)
	{
		Color color;
		_fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();

		switch(fadeType)
		{
			case 0:
				// FADE-OUT
				for(float i = 0; i < 1; i += Time.deltaTime / fadeTime)
				{
					color = new Color(0f, 0f, 0f, i);
					_fadeImage.color = color;
					yield return null;
				}

				break;

			case 1:
				// FADE-IN
				for(float i = 1; i > 0; i -= Time.deltaTime / fadeTime)
				{
					color = new Color(0f, 0f, 0f, i);
					_fadeImage.color = color;
					yield return null;
				}

				break;
		}
	}
}
