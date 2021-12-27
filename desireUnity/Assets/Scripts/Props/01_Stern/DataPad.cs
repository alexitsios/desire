public class DataPad : PropBase
{
	protected override string FancyName { get { return "Data Pad"; } }

	public override void Interact(ItemType item)
	{
		var blockName = "DataPad_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
