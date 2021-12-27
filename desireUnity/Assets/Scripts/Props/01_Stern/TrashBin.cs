public class TrashBin : PropBase
{
	protected override string FancyName { get { return "Trash Bin"; } }

	public override void Interact(ItemType item)
	{
		var blockName = "TrashBin_0";
		Flowchart.ExecuteBlock(blockName);

	}
}
