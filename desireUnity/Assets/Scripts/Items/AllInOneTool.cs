public class AllInOneTool : ItemBase
{
	public override void Interact()
	{
		var blockName = "AllInOneTool_0";
		Flowchart.ExecuteBlock(blockName);
		playerIventory.AddItem(new InventoryItem(itemType, itemImage));

		Destroy(gameObject);
	}
}
