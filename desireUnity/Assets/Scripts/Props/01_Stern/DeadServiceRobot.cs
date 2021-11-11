public class DeadServiceRobot : PropBase
{
	public override void Interact(ItemType item)
	{
		var blockName = "DeadServiceRobot_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
