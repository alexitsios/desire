public class DataPad : PropBase
{
	public override void Interact(ItemType item)
	{
		var blockName = "DataPad_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
