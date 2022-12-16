using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	private GameManager gameManager;

	[SerializeField] private TMP_Dropdown languageDropdown;
	[SerializeField] private Slider masterVolume;
	[SerializeField] private Slider FxVolume;
	[SerializeField] private Slider BgVolume;
	[Space]
	[SerializeField] private Button beepOn;
	[SerializeField] private Button beepOff, hintsOn, hintsOff;

	[SerializeField] private TMP_Text masterVolumeText, effectsVolumeText, backgroundVolumeText;
	[Space]
	[SerializeField] private Button backButton;

	private TMP_Text beepOnText, beepOffText, hintOnText, hintOffText;

	private Color off = new(1f, 1f, 1f, 0.5f);

	//private bool _finishedLoading = false;

	private void Start()
	{
		gameManager = GameManager.instance;

		//_finishedLoading = true;

		languageDropdown.value = PlayerPrefs.GetInt("Language", 1);
		masterVolume.value = PlayerPrefs.GetFloat("MasterVolume", 100f);
		FxVolume.value = PlayerPrefs.GetFloat("FxVolume", 100f);
		BgVolume.value = PlayerPrefs.GetFloat("BgVolume", 100f);

		ElementSettings();
		FindAdditionalComponents();		

		if (IntToBool(PlayerPrefs.GetInt("BeepSound")))
		{
			beepOnText.color = Color.white;
			beepOffText.color = off;
		}
		else
		{
			beepOnText.color = off;
			beepOffText.color = Color.white;
		}

		if (IntToBool(PlayerPrefs.GetInt("Hints")))
		{
			hintOnText.color = Color.white;
			hintOffText.color = off;
		}
		else
		{
			hintOnText.color = off;
			hintOffText.color = Color.white;
		}

		OnMasterVolumeChanged();
		OnFXVolumeChanged();
		OnBGVolumeChanged();

		//Caching these 
		//GameObject.Find("LanguageDropdown").GetComponent<TMP_Dropdown>().value = PlayerPrefs.GetInt("Language", 1);
		//GameObject.Find("Slider_0").GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume", 100f);
		//GameObject.Find("Slider_1").GetComponent<Slider>().value = PlayerPrefs.GetFloat("FxVolume", 100f);
		//GameObject.Find("Slider_2").GetComponent<Slider>().value = PlayerPrefs.GetFloat("BgVolume", 100f);
		//GameObject.Find("BeepToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Beep", 1) == 1;
		//GameObject.Find("HintsToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Hints", 0) == 1;

		//UpdateSliderLabel(0);
		//UpdateSliderLabel(1);
		//UpdateSliderLabel(2);
	}

	private void ElementSettings()
    {
		masterVolume.onValueChanged.AddListener(delegate { OnMasterVolumeChanged(); });
		FxVolume.onValueChanged.AddListener(delegate { OnFXVolumeChanged(); });
		BgVolume.onValueChanged.AddListener(delegate { OnBGVolumeChanged(); });

		beepOn.onClick.AddListener(delegate { ToggleBeep(true); });
		beepOff.onClick.AddListener(delegate { ToggleBeep(false); });

		hintsOn.onClick.AddListener(delegate { ToggleHints(true); });
		hintsOff.onClick.AddListener(delegate { ToggleHints(false); });

		backButton.onClick.AddListener(CloseSettingsScreen);
	}

	private void FindAdditionalComponents()
    {
		beepOnText = beepOn.GetComponent<TMP_Text>();
		beepOffText = beepOff.GetComponent<TMP_Text>();
		hintOnText = hintsOn.GetComponent<TMP_Text>();
		hintOffText = hintsOff.GetComponent<TMP_Text>();
	}

	/* Separated into OnMasterVolumeChaned, OnFXVolumeChanged, OnBGVolumeChanged
	public void UpdateSliderLabel(int sliderId)
	{
		if (!_finishedLoading)
		{
			return;
		}

		var value = GameObject.Find($"Slider_{sliderId}").GetComponent<Slider>().value;
		GameObject.Find($"Value_{sliderId}").GetComponent<TextMeshProUGUI>().text = value.ToString();
	}*/

	private void ToggleBeep(bool enable)
	{
		gameManager.Settings.BeepSound = enable;
		if (enable)
		{
			beepOnText.color = Color.white;
			beepOffText.color = off;
		}
		else
		{
			beepOnText.color = off;
			beepOffText.color = Color.white;
		}

		PlayerPrefs.SetInt("BeepSound", BoolToInt(enable));
	}

	private void ToggleHints(bool enable)
	{
		gameManager.Settings.ShowHints = enable;
		if (enable)
		{
			hintOnText.color = Color.white;
			hintOffText.color = off;
		}
		else
		{
			hintOnText.color = off;
			hintOffText.color = Color.white;
		}

		PlayerPrefs.SetInt("Hints", BoolToInt(enable));
	}

	private void OnMasterVolumeChanged()
    {
		var vol = masterVolume.value;
		gameManager.Settings.MasterVolume = vol;
		PlayerPrefs.SetFloat("MasterVolume", vol);
		if (masterVolumeText == null)
        {
			Debug.Log("Master null");
        }
		else
			masterVolumeText.text = vol.ToString();
	}

	private void OnFXVolumeChanged()
    {
		var vol = FxVolume.value;
		gameManager.Settings.FXVolume = vol;
		PlayerPrefs.SetFloat("FxVolume", vol);
		effectsVolumeText.text = vol.ToString();
	}

	private void OnBGVolumeChanged()
    {
		var vol = BgVolume.value;
		gameManager.Settings.BGVolume = vol;
		PlayerPrefs.SetFloat("BgVolume", vol);
		backgroundVolumeText.text = vol.ToString();
	}

	/* Separated into OnMasterVolumeChaned, OnFXVolumeChanged, OnBGVolumeChanged
	public void SaveSettings(string settingName)
	{
		if (!_finishedLoading)
		{
			return;
		}

		switch (settingName)
		{
			case "Language":
				var language = languageDropdown.value;
				gameManager.Settings.Language = (Language)language;
				PlayerPrefs.SetInt("Language", language);
				break;
			case "MasterVolume":
				{
					var vol = masterVolume.value;
					gameManager.Settings.MasterVolume = vol;
					PlayerPrefs.SetFloat("MasterVolume", vol);
					break;
				}
			case "FxVolume":
				{
					var vol = FxVolume.value;
					gameManager.Settings.FXVolume = vol;
					PlayerPrefs.SetFloat("FxVolume", vol);
					break;
				}
			case "BgVolume":
				{
					var vol = BgVolume.value;
					gameManager.Settings.BGVolume = vol;
					PlayerPrefs.SetFloat("BgVolume", vol);
					break;
				}
		}

		PlayerPrefs.Save();
	}*/

	private void CloseSettingsScreen()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}

	/* Removing and separating to ToggleBeep and ToggleHints
	public void SetToggleOn(string settingName)
	{
		string name;

		if(settingName == "BeepSound")
		{
			gameManager.Settings.BeepSound = true;
			name = "3";
		}
		else
		{
			gameManager.Settings.ShowHints = true;
			name = "4";
		}

		PlayerPrefs.SetInt(settingName, 1);
		GameObject.Find($"Value_{name}.1").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
		GameObject.Find($"Value_{name}.2").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0.5f);
	}

	public void SetToggleOff(string settingName)
	{
		string name;

		if(settingName == "BeepSound")
		{
			gameManager.Settings.BeepSound = false;
			name = "3";
		}
		else
		{
			gameManager.Settings.ShowHints = false;
			name = "4";
		}

		PlayerPrefs.SetInt(settingName, 0);
		GameObject.Find($"Value_{name}.1").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 0.5f);
		GameObject.Find($"Value_{name}.2").GetComponent<TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
	}*/

	//Returns 0 if false, else returns 1
	private int BoolToInt(bool b)
	{
		if (b == false) return 0;
		return 1;
	}

	//Returns false if 0, else returns true
	private bool IntToBool(int i)
	{
		if (i == 0) return false;
		return true;
	}
}
