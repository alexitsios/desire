using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
	Image fadeImage;
	Image image;
	TMP_Text text;
	GameManager gameManager;
	GameObject credits;

	[SerializeField]
	private int creditWaitTime = 15;

	public Sprite[] images;

	private void Start()
	{
		StartCoroutine(StartCutscene());
	}

	private IEnumerator FadeOut(bool total = true)
	{
		for(float i = 0; i <= (total == true ? 1 : 0.5); i += Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i);
			yield return null;
		}
	}

	private IEnumerator FadeIn()
	{
		for(float i = 1; i >= 0; i -= Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i);
			yield return null;
		}
	}

	private IEnumerator FadeTextIn()
	{
		text.gameObject.SetActive(true);

		for(float i = 0; i <= 1; i += Time.deltaTime / 2)
		{
			text.color = new Color(1f, 1f, 1f, i);
			yield return null;
		}
	}

	private IEnumerator FadeTextOut()
	{
		for(float i = 1; i >= 0; i -= Time.deltaTime / 2)
		{
			text.color = new Color(1f, 1f, 1f, i);
			yield return null;
		}

		text.gameObject.SetActive(false);
	}

	private IEnumerator StartCutscene()
	{
		yield return new WaitForEndOfFrame();

		credits = GameObject.Find("Credits");
		fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
		image = GameObject.Find("Image").GetComponent<Image>();
		text = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

		text.gameObject.SetActive(false);
		credits.SetActive(false);
		fadeImage.color = new Color(0f, 0f, 0f, 1f);

		yield return new WaitForSeconds(2);

		text.text = gameManager.TranslationManager.GetTranslatedEndingLine("@ending_1");

		yield return FadeTextIn();
		yield return new WaitForSeconds(2);
		yield return FadeTextOut();
		yield return FadeIn();
		yield return new WaitForSeconds(2);
		yield return FadeOut();

		image.sprite = images[1];
		text.text = gameManager.TranslationManager.GetTranslatedEndingLine("@ending_2");

		yield return FadeTextIn();
		yield return new WaitForSeconds(2);
		yield return FadeTextOut();
		yield return FadeIn();
		yield return new WaitForSeconds(2);
		yield return FadeOut();

		image.sprite = images[2];

		yield return new WaitForSeconds(2);
		yield return FadeIn();
		yield return new WaitForSeconds(2);
		yield return FadeOut(false);

		var line1 = $"- {gameManager.TranslationManager.GetTranslatedEndingLine("@ending_3")}\n";

		text.text = line1;

		yield return FadeTextIn();
		yield return new WaitForSeconds(2);
		yield return FadeTextOut();

		var line2 = line1 + $"- {gameManager.TranslationManager.GetTranslatedEndingLine("@ending_4")}";

		text.text = line2;

		yield return FadeTextIn();
		yield return new WaitForSeconds(2);
		yield return FadeTextOut();

		credits.SetActive(true);

		yield return new WaitForSeconds(creditWaitTime);

		SceneManager.LoadScene("00_StartGame");
	}
}
