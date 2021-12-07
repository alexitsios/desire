using UnityEngine;

public class VacuumRobot : NPCBase
{

	public GameObject trashBin;

	private int currentBlock = 0;

	public override void Interact(ItemType item)
	{
		var blockName = "VacuumRobot_" + currentBlock;
		Flowchart.ExecuteBlock(blockName);

		if(currentBlock == 0)
			currentBlock++;
	}
}