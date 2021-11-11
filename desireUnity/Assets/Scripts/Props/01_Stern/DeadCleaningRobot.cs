public class DeadCleaningRobot : PropBase
{
	public override void Interact(ItemType item)
	{
		var blockName = "DeadCleaningRobot_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
