public class Pothole : PropBase
{
	public override void Interact(ItemType item)
	{
		var blockName = "Pothole_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
