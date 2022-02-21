using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperstructureOutTranslation : TranslationBase
{
	public Dictionary<string, string> superstructure_out;

	public override string GetTranslatedLine(string lineKey)
	{
		return superstructure_out[lineKey];
	}
}
