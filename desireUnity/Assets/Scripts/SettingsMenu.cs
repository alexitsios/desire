using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	private bool _finishedLoading = false;

	private void Start()
	{
		_finishedLoading = true;

		GameObject.Find("LanguageDropdown").GetComponent<TMP_Dropdown>().value = PlayerPrefs.GetInt("Language", 1);
		GameObject.Find("Slider_0").GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume", 100f);
		GameObject.Find("Slider_1").GetComponent<Slider>().value = PlayerPrefs.GetFloat("FxVolume", 100f);
		GameObject.Find("Slider_2").GetComponent<Slider>().value = PlayerPrefs.GetFloat("BgVolume", 100f);
		//GameObject.Find("BeepToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Beep", 1) == 1;
		//GameObject.Find("HintsToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Hints", 0) == 1;

		UpdateSliderLabel(0);
		UpdateSliderLabel(1);
		UpdateSliderLabel(2);
	}

	public void UpdateSliderLabel(int sliderId)
	{
		if(!_finishedLoading)
		{
			return;
		}

		var value = GameObject.Find($"Slider_{sliderId}").GetComponent<Slider>().value;
		GameObject.Find($"Value_{sliderId}").GetComponent<TextMeshProUGUI>().text = value.ToString();
	}

	public void SaveSettings(string settingName)
	{
		if(!_finishedLoading)
		{
			return;
		}

		var gc = GetComponentInParent<GameManager>();

		switch(settingName)
		{
			case "Language":
				var language = GameObject.Find("LanguageDropdown").GetComponent<TMP_Dropdown>().value;
				gc.Settings.Language = (Language) language;
				PlayerPrefs.SetInt("Language", language);
				break;
			case "MasterVolume":
				var masterVolume = GameObject.Find("Slider_0").GetComponent<Slider>().value;
				gc.Settings.MasterVolume = masterVolume;
				PlayerPrefs.SetFloat("MasterVolume", masterVolume);
				break;
			case "FxVolume":
				var fxVolume = GameObject.Find("Slider_1").GetComponent<Slider>().value;
				gc.Settings.FXVolume = fxVolume;
				PlayerPrefs.SetFloat("FxVolume", fxVolume);
				break;
			case "BgVolume":
				var bgVolume = GameObject.Find("Slider_2").GetComponent<Slider>().value;
				gc.Settings.BGVolume = bgVolume;
				PlayerPrefs.SetFloat("BgVolume", bgVolume);
				break;
		}

		PlayerPrefs.Save();
	}

	public void CloseSettingsScreen()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}

	public void SetToggleOn(string settingName)
	{
		string name;

		var gc = GetComponentInParent<GameManager>();

		if(settingName == "BeepSound")
		{
			gc.Settings.BeepSound = true;
			name = "3";
		}
		else
		{
			gc.Settings.ShowHints = true;
			name = "4";
		}

		PlayerPrefs.SetInt(settingName, 1);
		GameObject.Find($"Value_{name}.1").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
		GameObject.Find($"Value_{name}.2").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0.5f);
	}

	public void SetToggleOff(string settingName)
	{
		string name;

		var gc = GetComponentInParent<GameManager>();

		if(settingName == "BeepSound")
		{
			gc.Settings.BeepSound = false;
			name = "3";
		}
		else
		{
			gc.Settings.ShowHints = false;
			name = "4";
		}

		PlayerPrefs.SetInt(settingName, 0);
		GameObject.Find($"Value_{name}.1").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0.5f);
		GameObject.Find($"Value_{name}.2").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
	}
}
