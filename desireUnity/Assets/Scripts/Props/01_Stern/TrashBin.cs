public class TrashBin : PropBase
{
	public override void Interact(ItemType item)
	{
		var blockName = "TrashBin_0";
		Flowchart.ExecuteBlock(blockName);

	}
}
