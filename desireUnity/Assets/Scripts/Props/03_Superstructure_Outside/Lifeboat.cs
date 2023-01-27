using Fungus;
using UnityEngine;

public class Lifeboat : PropBase
{
	protected override string FancyName { get { return "Lifeboat"; } }

	public override void Interact(ItemType item)
	{
		if (Debug.isDebugBuild && Input.GetKey(KeyCode.LeftAlt))
		{
			gameManager.StartEndingCutscene();
		}

		
        if(item == ItemType.ClearanceCard)
		{
			GameObject.Find("NPCsFlowchart").GetComponent<Flowchart>().ExecuteBlock("SecurityRobot");
		}
        else if(QuestController.Quests[QuestName.GetClearance] == QuestStatus.Completed && item == ItemType.NoItem)
        {
            gameManager.StartEndingCutscene();
        }
		else if(item == ItemType.NoItem)
		{
            GameObject.Find("NPCsFlowchart").GetComponent<Flowchart>().ExecuteBlock("SecurityRobot");
        }
        else
		{
			Flowchart.ExecuteBlock("ItemUseError");
		}
	}
}
