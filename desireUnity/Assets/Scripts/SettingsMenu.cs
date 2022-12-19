using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
	private GameManager gameManager;

	[SerializeField] private TMP_Dropdown languageDropdown;
	[SerializeField] private Slider masterVolume;
	[SerializeField] private Slider FxVolume;
	[SerializeField] private Slider BgVolume;
	[Space]
	[SerializeField] private Button beepOn;
	[SerializeField] private Button beepOff, hintsOn, hintsOff, backButton;

	[SerializeField] private TMP_Text masterVolumeText, effectsVolumeText, backgroundVolumeText;
	[Space]
	[SerializeField] private AudioClip testClip;

	private TMP_Text beepOnText, beepOffText, hintOnText, hintOffText;
	private AudioSource managerAudioSource;
	private Color off = new(1f, 1f, 1f, 0.5f);

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

	//Called by EventTrigger/OnPointerUp Event on each volume slider
	public void OnEndDrag(int sliderNumber)
    {
		if (managerAudioSource == null)
			managerAudioSource = GameManager.instance.gameObject.GetComponent<AudioSource>();
		float volume = 0;
		switch (sliderNumber)
        {
			case 0:
				Debug.Log("Master Volume Change");
				volume = gameManager.Settings.MasterVolume;
				break;
			case 1:
				Debug.Log("FX Volume Change");
				volume = gameManager.Settings.FXVolume;
				break;
			case 2:
				Debug.Log("BG Volume Change");
				volume = gameManager.Settings.BGVolume;
				break;
        }

		managerAudioSource.PlayOneShot(testClip, volume);
    }

	private void CloseSettingsScreen()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}

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
