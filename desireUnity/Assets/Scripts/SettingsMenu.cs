using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	private void Start()
	{
		GameObject.Find("LanguageSelect").GetComponent<TMP_Dropdown>().value = PlayerPrefs.GetInt("Language", 1);
		GameObject.Find("Slider_0").GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume", 100f);
		GameObject.Find("Slider_1").GetComponent<Slider>().value = PlayerPrefs.GetFloat("FxVolume", 100f);
		GameObject.Find("Slider_2").GetComponent<Slider>().value = PlayerPrefs.GetFloat("BgVolume", 100f);
		GameObject.Find("BeepToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Beep", 1) == 1;
		GameObject.Find("HintsToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Hints", 0) == 1;
	}

	public void UpdateSliderLabel(int sliderId)
	{
		var value = GameObject.Find($"Slider_{sliderId}").GetComponent<Slider>().value;
		GameObject.Find($"SliderLabel_{sliderId}").GetComponent<TextMeshProUGUI>().text = value.ToString();
	}

	public void SaveSettings()
	{
		var language = GameObject.Find("LanguageSelect").GetComponent<TMP_Dropdown>().value;
		var masterVolume = GameObject.Find("Slider_0").GetComponent<Slider>().value;
		var fxVolume = GameObject.Find("Slider_1").GetComponent<Slider>().value;
		var bgVolume = GameObject.Find("Slider_2").GetComponent<Slider>().value;
		var beep = GameObject.Find("BeepToggle").GetComponent<Toggle>().isOn ? 1 : 0;
		var hints = GameObject.Find("HintsToggle").GetComponent<Toggle>().isOn ? 1 : 0;

		var gc = GetComponentInParent<GameManager>();
		gc.Settings = new Settings
		{
			Language = (Language) language,
			MasterVolume = masterVolume,
			FXVolume = fxVolume,
			BGVolume = bgVolume,
			BeepSound = beep == 1,
			ShowHints = hints == 1
		};

		PlayerPrefs.SetInt("Language", language);
		PlayerPrefs.SetFloat("MasterVolume", masterVolume);
		PlayerPrefs.SetFloat("FxVolume", fxVolume);
		PlayerPrefs.SetFloat("BgVolume", bgVolume);
		PlayerPrefs.SetInt("Beep", beep);
		PlayerPrefs.SetInt("Hints", hints);
		PlayerPrefs.Save();

		CloseSettingsScreen();
	}

	public void CloseSettingsScreen()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}
}
