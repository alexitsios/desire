public class Horizon : PropBase
{
	public override void Interact(ItemType item)
	{
		var blockName = "Horizon_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
