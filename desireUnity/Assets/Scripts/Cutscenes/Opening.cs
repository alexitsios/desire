using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    private RawImage fadeImage;
    private TMP_Text text1;
    private TMP_Text text2;
	private GameManager gameManager;

	private void Start()
	{
		fadeImage = GameObject.Find("FadeImage").GetComponent<RawImage>();
		text1 = GameObject.Find("Text1").GetComponent<TMP_Text>();
		text2 = GameObject.Find("Text2").GetComponent<TMP_Text>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

		text1.text = gameManager.TranslationManager.GetTranslatedCredit("presents");
		text2.text = gameManager.TranslationManager.GetTranslatedCredit("title");
		text2.gameObject.SetActive(false);

		StartCoroutine(StartCutscene());
	}

	private IEnumerator StartCutscene()
	{
		yield return new WaitForSeconds(2);

		for(float i = 1; i >= 0; i -= Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i); ;
			yield return null;
		}

		yield return new WaitForSeconds(2);

		for(float i = 0; i <= 1; i += Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i); ;
			yield return null;
		}

		text1.gameObject.SetActive(false);
		text2.gameObject.SetActive(true);

		yield return new WaitForSeconds(3);

		for(float i = 1; i >= 0; i -= Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i); ;
			yield return null;
		}

		yield return new WaitForSeconds(3);

		for(float i = 0; i <= 1; i += Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i); ;
			yield return null;
		}

		StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().LoadSceneAndSpawnPlayer(SceneName.Stern, 1, false));
	}
}
