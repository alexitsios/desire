using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelTranslation : TranslationBase
{
	public Dictionary<string, string> funnel;

	public override string GetTranslatedLine(string lineKey)
	{
		return funnel[lineKey];
	}
}
