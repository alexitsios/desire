public class Panel : PropBase
{
	public override void Interact(ItemType item)
	{
		var blockName = "Panel_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
