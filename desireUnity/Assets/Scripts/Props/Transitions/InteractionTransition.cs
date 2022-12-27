using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

	public bool checkVariableBeforeTransition = false;
	public string variableName;
	public string blockOnError;

    public override void OnMouseEnter()
    {
		gameManager.SetCursorAction(cursorAction);
		gameManager.SetInteractDialogText(FancyName);
	}

    /*Switched to OnMouseEnter
     * public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		gameManager.SetCursorAction(cursorAction);
		gameManager.SetInteractDialogText(FancyName);
	}*/

	public override void Interact(ItemType item)
	{
		if(checkVariableBeforeTransition)
		{
			if(!gameManager.GetComponent<InkManager>().GetVariable<bool>(variableName))
			{
				Flowchart.ExecuteBlock(blockOnError);
				return;
			}
		}

		StartCoroutine(gameManager.LoadSceneAndSpawnPlayer(transitionTo, spawnPoint));
	}
}
