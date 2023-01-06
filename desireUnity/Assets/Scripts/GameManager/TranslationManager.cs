using Newtonsoft.Json;
using UnityEngine;

public class TranslationManager : MonoBehaviour
{
	public TextAsset[] translationFiles;

	private TextAsset _selectedFile;
	private static TranslationBase _sceneTranslation;
	private static GeneralTranslation _baseTranslation;

	private void Start()
	{
		LoadBaseTranslation();
	}

	public void LoadBaseTranslation()
	{
		_selectedFile = translationFiles[(int) GetComponent<GameManager>().Settings.Language];

		_baseTranslation = JsonConvert.DeserializeObject<GeneralTranslation>(_selectedFile.text);
	}

	public void LoadSceneTranslation(Language language, SceneName scene)
	{
		_selectedFile = translationFiles[(int) language];

		switch(scene)
		{
			case SceneName.Stern:
				_sceneTranslation = JsonConvert.DeserializeObject<SternTranslation>(_selectedFile.text);
				break;
			case SceneName.Funnel:
				_sceneTranslation = JsonConvert.DeserializeObject<FunnelTranslation>(_selectedFile.text);
				break;
			case SceneName.Superstructure_out:
				_sceneTranslation = JsonConvert.DeserializeObject<SuperstructureOutTranslation>(_selectedFile.text);
				break;
			case SceneName.Superstructure_in:
				_sceneTranslation = JsonConvert.DeserializeObject<SuperstructureInTranslation>(_selectedFile.text);
				break;
			case SceneName.Generator_room:
				_sceneTranslation = JsonConvert.DeserializeObject<GeneratorRoomTranslation>(_selectedFile.text);
				break;
			case SceneName.Bridge:
				_sceneTranslation = JsonConvert.DeserializeObject<BridgeTranslation>(_selectedFile.text);
				break;
		}
	}

	public string GetTranslatedName(string nameKey)
	{
		return _baseTranslation.characters[nameKey];
	}

	public string GetTranslatedProp(string propKey)
	{
		return _baseTranslation.props[propKey];
	}

	public string GetTranslatedItem(string itemKey)
	{
		return _baseTranslation.items[itemKey];
	}

	public string GetTranslatedItem(ItemType itemType)
	{
		return GetTranslatedItem(itemType.ToString());
	}

	public string GetTranslatedSpecial(string key)
	{
		return _baseTranslation.special[key];
	}

	public string GetTranslatedLine(string lineKey)
	{
		if(lineKey.StartsWith('@'))
		{
			return _sceneTranslation.GetTranslatedLine(lineKey);
		}

		return GetTranslatedSpecial(lineKey);
	}

	public string GetTranslatedUi(string uiKey)
	{
		return _baseTranslation.ui[uiKey];
	}

	public string GetTranslatedCredit(string creditKey)
	{
		return _baseTranslation.credits[creditKey];
	}
}
