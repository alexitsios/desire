using UnityEngine;

public class DeadServiceRobot : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp("@dead_service_robot"); } }

	public override void Interact(ItemType item)
	{
		string blockName;

		// Retrieves the arm if the All-In-One Tool is used here
		if(item == ItemType.AllInOneTool)
		{
			blockName = "DeadServiceRobot_1";
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().AcquiredArm = true;
		}
		// Retrieves the memory drive
		else if(item == ItemType.ServiceKit)
			blockName = "DeadServiceRobot_2";
		else
			blockName = "DeadServiceRobot_0";

		Flowchart.ExecuteBlock(blockName);
	}
}
