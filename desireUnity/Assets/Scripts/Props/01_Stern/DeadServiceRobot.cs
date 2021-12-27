public class DeadServiceRobot : PropBase
{
	protected override string FancyName { get { return "Dead Service Robot"; } }

	public override void Interact(ItemType item)
	{
		string blockName;

		// Retrieves the arm if the All-In-One Tool is used here
		if(item == ItemType.AllInOneTool)
			blockName = "DeadServiceRobot_1";
		else if(item == ItemType.NoItem)
			blockName = "DeadServiceRobot_0";
		else
			blockName = "DeadServiceRobot_0";

		Flowchart.ExecuteBlock(blockName);
	}
}
