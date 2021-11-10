public class Doors : PropBase
{
	public override void Interact()
	{
		var blockName = "Doors_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
