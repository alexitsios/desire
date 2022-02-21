public class DataPad : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp("@data_pad"); } }

	public override void Interact(ItemType item)
	{
		var blockName = "DataPad_0";
		Flowchart.ExecuteBlock(blockName);
	}
}
