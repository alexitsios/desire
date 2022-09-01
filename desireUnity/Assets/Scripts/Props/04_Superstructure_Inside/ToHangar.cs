public class ToHangar : PropBase
{
    protected override string FancyName => TranslationManager.GetTranslatedProp("@to_hangar");

    public override void Interact(ItemType item)
    {
        Flowchart.ExecuteBlock("ToHangar");
    }
}
