using UnityEngine;

public class VacuumRobot : NPCBase
{

	public GameObject trashBin;
	private float speed;

	public void CleanTrashBin()
	{
		speed = 2;
		canMove = true;
		GoTo(trashBin.transform.position, speed);
	}

	public override void Interact()
	{
		var blockName = "VacuumRobot_0";
		Flowchart.ExecuteBlock(blockName);
	}
}