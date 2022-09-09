using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeboatSlots : PropBase
{
    protected override string FancyName { get { return "Empty Lifeboat Slot"; } }

	public override void Interact(ItemType item)
	{
		Flowchart.ExecuteBlock("LifeboatSlots");
	}
}