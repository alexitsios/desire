using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObtainedPopup : MonoBehaviour
{
	private GameManager gameManager;

	public Image itemSprite;
	public TMP_Text itemName;
	public TMP_Text dialogBody;

	public void SetDialog(InventoryItem itemObtained)
	{
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		dialogBody.text = gameManager.TranslationManager.GetTranslatedUi("NewItem");

		var name = gameManager.TranslationManager.GetTranslatedItem(itemObtained._type);

		itemName.text = name;
		itemSprite.sprite = itemObtained._sprite;
	}
}
