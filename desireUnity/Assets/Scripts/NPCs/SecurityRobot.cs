using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityRobot : NPCBase
{
   protected override string FancyName { get { return "Security Robot"; } }

	public override void Interact(ItemType item)
	{
		if(item == ItemType.NoItem || item == ItemType.ClearanceCard)
		{
			var blockName = gameObject.name;
			Flowchart.ExecuteBlock(blockName);
		}
		else
		{
			Flowchart.ExecuteBlock("ItemUseError");
		}
	}
}
