public class Pothole : PropBase
{
	public override void Interact()
	{
		var blockName = "Pothole_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
