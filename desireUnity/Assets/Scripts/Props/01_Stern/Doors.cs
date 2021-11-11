using UnityEngine.SceneManagement;

public class Doors : PropBase
{
	public override void Interact(ItemType item)
	{
		// Checks if the left doors are open
		if(QuestController.Quests["SternDoorOpened"] == QuestStatus.Completed)
		{
			// TODO: This needs to change
			SceneManager.LoadScene(3);
		}
		else
		{
			var blockName = "Doors_0";
			Flowchart.ExecuteBlock(blockName);
		}
	}
}
