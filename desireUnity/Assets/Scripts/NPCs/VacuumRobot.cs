using UnityEngine;

public class VacuumRobot : NPCBase
{

	public GameObject trashBin;

	private float speed;
	private int currentBlock = 0;

	public void CleanTrashBin()
	{
		speed = 2;
		canMove = true;
		GoTo(trashBin.transform.position, speed);
	}

	public override void Interact()
	{
		var blockName = "VacuumRobot_" + currentBlock;
		Flowchart.ExecuteBlock(blockName);

		if(currentBlock == 0)
			currentBlock++;
	}
}