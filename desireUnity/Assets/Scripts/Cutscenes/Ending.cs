using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
	Image fadeImage;
	TMP_Text credits;
	[SerializeField]
	private int creditWaitTime = 4;

	private void Start()
	{
		StartCoroutine(StartCutscene());
	}

	private IEnumerator StartCutscene()
	{
		yield return new WaitForEndOfFrame();

		fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
		credits = GameObject.Find("Credits").GetComponent<TMP_Text>();

		credits.gameObject.SetActive(false);
		fadeImage.color = new Color(0f, 0f, 0f, 1f);

		yield return new WaitForSeconds(2);

		for(float i = 1; i >= 0; i -= Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i); ;
			yield return null;
		}

		yield return new WaitForSeconds(4);

		for(float i = 0; i <= 1; i += Time.deltaTime / 2)
		{
			fadeImage.color = new Color(0f, 0f, 0f, i); ;
			yield return null;
		}

		credits.gameObject.SetActive(true);

		yield return new WaitForSeconds(creditWaitTime);

		SceneManager.LoadScene("00_StartGame");
	}
}
