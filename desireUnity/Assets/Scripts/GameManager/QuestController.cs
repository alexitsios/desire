using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public Dictionary<string, QuestStatus> Quests = new Dictionary<string, QuestStatus>();

	private void Start()
	{
		Quests.Add("RecoveredArm", QuestStatus.Active);
		Quests.Add("RecoveredLeg", QuestStatus.Active);
		Quests.Add("SternDoorOpened", QuestStatus.Inactive);
	}

	public void SetQuestStatus(string questName, QuestStatus questStatus)
	{
		Quests[questName] = questStatus;
	}
}
