using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Doors : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp("@doors"); } }

	public override void Interact(ItemType item)
	{
		// Checks if the left doors are open
		if(QuestController.Quests[QuestName.OpenSternDoor] == QuestStatus.Completed)
		{
			gameManager.LoadSceneAndSpawnPlayer(SceneName.Funnel, 1);
		}
		else
		{
			var blockName = "Doors_0";
			Flowchart.ExecuteBlock(blockName);
		}
	}

	public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		if(QuestController.Quests[QuestName.OpenSternDoor] == QuestStatus.Completed)
		{
			gameManager.SetCursorAction(CursorAction.LeftArrow);
			gameManager.SetInteractDialogText("Go to Funnel");
		}
		else
			base.OnPointerEnter(pointerEventData);
	}
}
