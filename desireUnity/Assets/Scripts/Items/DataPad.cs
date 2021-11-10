public class DataPad : ItemBase
{
	public override void Interact()
	{
		var blockName = "DataPad_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
