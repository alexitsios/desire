using System.Collections.Generic;

public class SternTranslation : TranslationBase
{
	public Dictionary<string, string> stern;

	public override string GetTranslatedLine(string lineKey)
	{
		return stern[lineKey];
	}
}
