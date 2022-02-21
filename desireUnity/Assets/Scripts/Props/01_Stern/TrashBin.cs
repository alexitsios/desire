public class TrashBin : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp("@trash_bin"); } }

	public override void Interact(ItemType item)
	{
		var blockName = "TrashBin_0";
		Flowchart.ExecuteBlock(blockName);

	}
}
