using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lifeboat : PropBase
{
    protected override string FancyName { get { return "Lifeboat"; } }

	public override void Interact(ItemType item)
	{
		if(QuestController.Quests[QuestName.GetClearance] == QuestStatus.Completed)
		{
			gameManager.StartEndingCutscene();
		} else
		{
			base.Interact(item);
		}
	}
}
