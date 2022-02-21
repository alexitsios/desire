using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperstructureInTranslation : TranslationBase
{
	public Dictionary<string, string> superstructure_in;

	public override string GetTranslatedLine(string lineKey)
	{
		return superstructure_in[lineKey];
	}
}
