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
			{ SceneName.Funnel, "funnel_default" },
			{ SceneName.Superstructure_out, "superstructure_out_default" },
			{ SceneName.Superstructure_in, "superstructure_in_default" },
			{ SceneName.Generator_room, "generator_room_default" },
			{ SceneName.Bridge, "bridge_default" }
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

	public IEnumerator Fade(string type, float time)
	{
		int multiplier = (type == "in" || type == "dim-in") ? -1 : 1;
		float start = type == "in" ? 1 : type == "out" ? 0 : type == "dim-in" ? 0.7f : 0;
		float target = type == "in" ? 0 : type == "out" ? 1 : type == "dim-in" ? 0f : 0.7f;

		_fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();

		for(float i = start; i * multiplier <= target; i += (Time.deltaTime * multiplier) / time)
		{
			_fadeImage.color = new Color(0f, 0f, 0f, i);
			yield return null;
		}
	}
}
