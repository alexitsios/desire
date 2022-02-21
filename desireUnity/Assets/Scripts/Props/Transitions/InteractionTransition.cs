using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionTransition : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp(translationString); } }

	public CursorAction cursorAction;
	public SceneName transitionTo;
	[Min(1)]
	public int spawnPoint;
	public string translationString;

	public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		gameManager.SetCursorAction(cursorAction);
		gameManager.SetInteractDialogText(FancyName);
	}

	public override void Interact(ItemType item)
	{
		gameManager.LoadSceneAndSpawnPlayer(transitionTo, spawnPoint);
	}
}
