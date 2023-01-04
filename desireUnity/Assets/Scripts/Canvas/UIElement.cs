using TMPro;
using UnityEngine;

public class UIElement : MonoBehaviour
{
	public TMP_Text label;
	public string labelId;

	private void Start()
	{
		label.text = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().TranslationManager.GetTranslatedUi(labelId);
	}
}
