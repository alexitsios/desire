using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public void UpdateSliderLabel(int sliderId)
	{
		var value = GameObject.Find($"Slider_{sliderId}").GetComponent<Slider>().value;
		GameObject.Find($"SliderLabel_{sliderId}").GetComponent<TextMeshProUGUI>().text = value.ToString();
	}

	public void SaveSettings()
	{
		var gc = GetComponentInParent<GameManager>();
		gc.Settings = new Settings
		{
			Language = (Language)GameObject.Find("LanguageSelect").GetComponent<TMP_Dropdown>().value,
			MasterVolume = GameObject.Find("Slider_0").GetComponent<Slider>().value / 100,
			FXVolume = GameObject.Find("Slider_1").GetComponent<Slider>().value / 100,
			BGVolume = GameObject.Find("Slider_2").GetComponent<Slider>().value / 100,
			BeepSound = GameObject.Find("BeepToggle").GetComponent<Toggle>().isOn,
			ShowHints = GameObject.Find("HintsToggle").GetComponent<Toggle>().isOn
		};

		CloseSettingsScreen();
	}

	public void CloseSettingsScreen()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}
}
