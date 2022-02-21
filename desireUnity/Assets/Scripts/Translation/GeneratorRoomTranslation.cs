using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRoomTranslation : TranslationBase
{
	public Dictionary<string, string> generator_room;

	public override string GetTranslatedLine(string lineKey)
	{
		return generator_room[lineKey];
	}
}
