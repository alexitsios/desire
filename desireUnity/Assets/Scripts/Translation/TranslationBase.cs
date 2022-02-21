using System;
using System.Collections.Generic;

[Serializable]
public abstract class TranslationBase
{
	public Dictionary<string, string> characters;
	public Dictionary<string, string> items;
	public Dictionary<string, string> props;
	public Dictionary<string, string> special;

	public abstract string GetTranslatedLine(string lineKey);
}
