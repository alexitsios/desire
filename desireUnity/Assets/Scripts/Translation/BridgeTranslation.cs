using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTranslation : TranslationBase
{
	public Dictionary<string, string> bridge;

	public override string GetTranslatedLine(string lineKey)
	{
		return bridge[lineKey];
	}
}
