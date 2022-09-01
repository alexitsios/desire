using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamedDoor : PropBase
{
	protected override string FancyName => TranslationManager.GetTranslatedProp("@jamed_door");

	public override void Interact(ItemType item)
	{
		Flowchart.ExecuteBlock("JamedDoor");
	}
}
