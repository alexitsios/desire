using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

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

	[SerializeField] private TMP_Text masterVolumeValue, effectsVolumeValue, backgroundVolumeValue;
	[Space]
	[SerializeField] private AudioClip testClip;

	private TMP_Text beepOnText, beepOffText, hintOnText, hintOffText;
	private MusicManager musicManager;
	//private AudioSource managerAudioSource;
	private Color off = new(1f, 1f, 1f, 0.5f);

	private void Start()
	{
		gameManager = GameManager.instance;
		musicManager = GameObject.Find("FungusManager").GetComponent<MusicManager>();
		//_finishedLoading = true;

		languageDropdown.value = PlayerPrefs.GetInt("Language", 0);
		masterVolume.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
		FxVolume.value = PlayerPrefs.GetFloat("FxVolume", 1f);
		BgVolume.value = PlayerPrefs.GetFloat("BgVolume", 1f);

		ElementSettings();
		FindAdditionalComponents();

		if(IntToBool(PlayerPrefs.GetInt("BeepSound")))
		{
			beepOnText.color = Color.white;
			beepOffText.color = off;
		}
		else
		{
			beepOnText.color = off;
			beepOffText.color = Color.white;
		}

		if(IntToBool(PlayerPrefs.GetInt("Hints")))
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
		languageDropdown.onValueChanged.AddListener(delegate
		{
			//OnLanguageChanged();
		});

		masterVolume.onValueChanged.AddListener(delegate
		{ 
			OnMasterVolumeChanged(); 
		});

		FxVolume.onValueChanged.AddListener(delegate
		{ 
			OnFXVolumeChanged(); 
		});

		BgVolume.onValueChanged.AddListener(delegate
		{ 
			OnBGVolumeChanged(); 
		});

		beepOn.onClick.AddListener(delegate
		{ 
			ToggleBeep(true); 
		});

		beepOff.onClick.AddListener(delegate
		{ 
			ToggleBeep(false); 
		});

		hintsOn.onClick.AddListener(delegate
		{ 
			ToggleHints(true); 
		});

		hintsOff.onClick.AddListener(delegate
		{ 
			ToggleHints(false); 
		});

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
		if(enable)
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
		if(enable)
		{
			hintOnText.color = Color.white;
			hintOffText.color = off;
		}
		else
		{
			hintOnText.color = off;
			hintOffText.color = Color.white;
		}

        foreach (var prop in FindObjectsOfType<PropBase>())
        {
            prop.DisplayHints = enable;
        }

        PlayerPrefs.SetInt("Hints", BoolToInt(enable));
	}

	private void OnLanguageChanged()
	{
		var language = languageDropdown.value;

		gameManager.Settings.Language = (Language) language;
		gameManager.TranslationManager.LoadBaseTranslation();

		PlayerPrefs.SetInt("Language", language);
	}

	private void OnMasterVolumeChanged()
	{
		var vol = masterVolume.value;
		gameManager.Settings.MasterVolume = vol;
		PlayerPrefs.SetFloat("MasterVolume", vol);
		//I don't know why I need to check if it's null when it's assigned in inspector
		//but for some reason it's throwing an error as being null maybe half of the time
		//and yet it still works... IDK 
		if (masterVolumeValue != null) 
		{
			masterVolumeValue.text = Mathf.RoundToInt(vol * 100).ToString();
		}

		musicManager.OnVolumesChanged(gameManager.Settings.MasterVolume, gameManager.Settings.BGVolume, gameManager.Settings.FXVolume);
	}

	private void OnFXVolumeChanged()
	{
		var vol = FxVolume.value;
		gameManager.Settings.FXVolume = vol;
		PlayerPrefs.SetFloat("FxVolume", vol);
		if (effectsVolumeValue != null)
        {
			effectsVolumeValue.text = Mathf.RoundToInt(vol * 100).ToString();
		}

		musicManager.OnVolumesChanged(gameManager.Settings.MasterVolume, gameManager.Settings.BGVolume, gameManager.Settings.FXVolume);
	}

	private void OnBGVolumeChanged()
	{
		var vol = BgVolume.value;
		gameManager.Settings.BGVolume = vol;
		PlayerPrefs.SetFloat("BgVolume", vol);
		if (backgroundVolumeValue != null)
        {
			backgroundVolumeValue.text = Mathf.RoundToInt(vol * 100).ToString();
		}

		musicManager.OnVolumesChanged(gameManager.Settings.MasterVolume, gameManager.Settings.BGVolume, gameManager.Settings.FXVolume);
	}

	//Called by EventTrigger/OnPointerUp Event on each volume slider
	public void OnEndDrag(int sliderNumber)
	{
		if(musicManager == null)
        {
			musicManager = GameObject.Find("FungusManager").GetComponent<MusicManager>();
		}

		float volume = 0;
		switch(sliderNumber)
		{
			case 0:
				//Debug.Log("Master Volume Change");
				volume = gameManager.Settings.MasterVolume;
				break;
			case 1:
				//Debug.Log("FX Volume Change");
				volume = gameManager.Settings.FXVolume * gameManager.Settings.MasterVolume;
				break;
			case 2:
				//Debug.Log("BG Volume Change");
				volume = gameManager.Settings.BGVolume * gameManager.Settings.MasterVolume;
				//volume = Mathf.Lerp(0f, 1f, (gameManager.Settings.BGVolume + gameManager.Settings.MasterVolume) / 2);
				break;
		}

		if (testClip != null && musicManager != null)
        {
			musicManager.PlaySound(testClip, volume, true);
        }
	}

	private void CloseSettingsScreen()
	{
		Destroy(gameObject);
	}

	//Returns 0 if false, else returns 1
	private int BoolToInt(bool b)
	{
		if(b == false)
        {
			return 0;
        }
		return 1;
	}

	//Returns false if 0, else returns true
	private bool IntToBool(int i)
	{
		if(i == 0)
		{
			return false;
		}
		return true;
	}
}
