public class Panel : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp("@pannel"); } }

	public override void Interact(ItemType item)
	{
		string blockName;

		if(item == ItemType.AllInOneTool)
			blockName = "Pannel_1";
		else
			blockName = "Pannel_0";

		Flowchart.ExecuteBlock(blockName);
	}
}
