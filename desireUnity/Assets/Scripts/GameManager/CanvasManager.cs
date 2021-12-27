using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	[Serializable]
	public struct Background
	{
		public string name;
		public Sprite sprite;
	}

	private SpriteRenderer _background;
	private Image _fadeImage;
	private Dictionary<SceneName, string> CurrentBackgrounds;

	public Background[] backgrounds;

	private void Awake()
	{
		CurrentBackgrounds = new Dictionary<SceneName, string>
		{
			{ SceneName.Stern, "stern_default" },
			{ SceneName.Funnel, "funnel_default" }
		};
	}

	public void StartCanvasManager()
	{
		_background = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
	}

	public void UpdateBackground(string bgName)
	{
		foreach(var bg in backgrounds)
		{
			if(bg.name == bgName)
			{
				var sceneName = (SceneName) SceneManager.GetActiveScene().buildIndex;
				_background.sprite = bg.sprite;
				CurrentBackgrounds[sceneName] = bgName;
			}
		}
	}

	public void LoadLastBackground(SceneName scene)
	{
		var bg = CurrentBackgrounds[scene];
		UpdateBackground(bg);
	}

	public IEnumerator Fade(int fadeType, float fadeTime)
	{
		Color color;
		_fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();

		switch(fadeType)
		{
			case 0:
				// FADE-OUT
				for(float i = 0; i <= 1; i += Time.deltaTime / fadeTime)
				{
					color = new Color(0f, 0f, 0f, i);
					_fadeImage.color = color;
					yield return null;
				}

				break;

			case 1:
				// FADE-IN
				for(float i = 1; i >= 0; i -= Time.deltaTime / fadeTime)
				{
					color = new Color(0f, 0f, 0f, i);
					_fadeImage.color = color;
					yield return null;
				}

				break;
		}
	}
}
