public class AllInOneTool : ItemBase
{
	protected override string FancyName { get { return "All-in-One Tool"; } }

	public override void Interact(ItemType item)
	{
		var blockName = "AllInOneTool_0";
		Flowchart.ExecuteBlock(blockName);

		Destroy(gameObject);
	}
}
