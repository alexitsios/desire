using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Doors : PropBase
{
	protected override string FancyName { get { return "Doors"; } }

	public override void Interact(ItemType item)
	{
		// Checks if the left doors are open
		if(QuestController.Quests[QuestName.OpenSternDoor] == QuestStatus.Completed)
		{
			// TODO: This needs to change
			SceneManager.LoadScene((int) SceneName.Funnel);
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
