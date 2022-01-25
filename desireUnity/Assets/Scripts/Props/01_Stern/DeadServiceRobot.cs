public class DeadServiceRobot : PropBase
{
	protected override string FancyName { get { return "Dead Service Robot"; } }

	public override void Interact(ItemType item)
	{
		string blockName;

		// Retrieves the arm if the All-In-One Tool is used here
		if(item == ItemType.AllInOneTool)
			blockName = "DeadServiceRobot_1";
		// Retrieves the memory drive
		else if(item == ItemType.ServiceKit)
			blockName = "DeadServiceRobot_2";
		else
			blockName = "DeadServiceRobot_0";

		Flowchart.ExecuteBlock(blockName);
	}
}
