using UnityEngine;

public class VacuumRobot : NPCBase
{
	private int currentBlock = 0;
	protected override string FancyName { get { return "Vacuum Robot"; } }

	public override void Interact(ItemType item)
	{
		var blockName = "VacuumRobot_" + currentBlock;
		Flowchart.ExecuteBlock(blockName);

		if(currentBlock == 0)
			currentBlock++;
	}
}