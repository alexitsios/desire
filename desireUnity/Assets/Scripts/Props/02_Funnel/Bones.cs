public class Bones : PropBase
{
	protected override string FancyName { get { return "Bones"; } }

	public override void Interact(ItemType item)
	{
		Flowchart.ExecuteBlock("Bones");
	}
}
