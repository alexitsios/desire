using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TranslationManager : MonoBehaviour
{
	public TextAsset[] translationFiles;

	private TextAsset _selectedFile;
	private static Translation _translation;

	public void LoadTranslation(Language language)
	{
		_selectedFile = translationFiles[(int) language];

		_translation = JsonUtility.FromJson<Translation>(_selectedFile.text);
	}

	public string GetTranslatedLine(SceneName scene, int lineIndex)
	{
		string[] selectedArray = null;

		switch(scene)
		{
			case SceneName.Stern:
				selectedArray = _translation.stern;
				break;
		}

		return selectedArray[lineIndex];
	}
}
