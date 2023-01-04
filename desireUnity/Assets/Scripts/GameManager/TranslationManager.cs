using Newtonsoft.Json;
using UnityEngine;

public class TranslationManager : MonoBehaviour
{
	public TextAsset[] translationFiles;

	private TextAsset _selectedFile;
	private static TranslationBase _translation;

	public void LoadTranslation(Language language, SceneName scene)
	{
		_selectedFile = translationFiles[(int) language];

		switch(scene)
		{
			case SceneName.Stern:
				_translation = JsonConvert.DeserializeObject<SternTranslation>(_selectedFile.text);
				break;
			case SceneName.Funnel:
				_translation = JsonConvert.DeserializeObject<FunnelTranslation>(_selectedFile.text);
				break;
			case SceneName.Superstructure_out:
				_translation = JsonConvert.DeserializeObject<SuperstructureOutTranslation>(_selectedFile.text);
				break;
			case SceneName.Superstructure_in:
				_translation = JsonConvert.DeserializeObject<SuperstructureInTranslation>(_selectedFile.text);
				break;
			case SceneName.Generator_room:
				_translation = JsonConvert.DeserializeObject<GeneratorRoomTranslation>(_selectedFile.text);
				break;
			case SceneName.Bridge:
				_translation = JsonConvert.DeserializeObject<BridgeTranslation>(_selectedFile.text);
				break;
		}
	}

	public string GetTranslatedName(string nameKey)
	{
		return _translation.characters[nameKey];
	}

	public string GetTranslatedProp(string propKey)
	{
		return _translation.props[propKey];
	}

	public string GetTranslatedItem(string itemKey)
	{
		return _translation.items[itemKey];
	}

	public string GetTranslatedItem(ItemType itemType)
	{
		return GetTranslatedItem(itemType.ToString());
	}

	public string GetTranslatedSpecial(string key)
	{
		return _translation.special[key];
	}

	public string GetTranslatedLine(string lineKey)
	{
		if(lineKey.StartsWith('@'))
		{
			return _translation.GetTranslatedLine(lineKey);
		}

		return GetTranslatedSpecial(lineKey);
	}

	public string GetTranslatedUi(string uiKey)
	{
		return _translation.ui[uiKey];
	}
}
