using System;
using System.Collections.Generic;

[Serializable]
public abstract class TranslationBase
{
	public abstract string GetTranslatedLine(string lineKey);
}
