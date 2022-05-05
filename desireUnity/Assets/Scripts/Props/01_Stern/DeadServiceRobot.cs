using UnityEngine;

public class DeadServiceRobot : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp("@dead_service_robot"); } }

	private bool usedAllInOneTool = false;
	private bool usedServiceKit = false;

	public override void Interact(ItemType item)
	{
		string blockName;

		if(item == ItemType.AllInOneTool && !usedAllInOneTool)
		{
			// Retrieves the arm
			usedAllInOneTool = true;
			blockName = "DeadServiceRobot_1";
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().AcquiredArm = true;
		}
		else if(item == ItemType.ServiceKit && !usedServiceKit)
		{
			// Retrieves the memory drive
			usedServiceKit = true;
			blockName = "DeadServiceRobot_2";
		}
		else
			blockName = "DeadServiceRobot_0";

		Flowchart.ExecuteBlock(blockName);
	}
}
