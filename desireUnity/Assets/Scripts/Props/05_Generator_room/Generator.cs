using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : PropBase
{
	protected override string FancyName { get { return TranslationManager.GetTranslatedProp("@generator"); } }

	public override void Interact(ItemType item)
	{
		if(item == ItemType.ServiceKit)
		{
			Flowchart.ExecuteBlock("Generator_fix");
		}
		else if(item == ItemType.NoItem)
		{
			Flowchart.ExecuteBlock("Generator_look");
		}
	}
}
