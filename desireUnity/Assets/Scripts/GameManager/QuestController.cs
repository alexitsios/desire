using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public Dictionary<QuestName, QuestStatus> Quests { get; set; } = new Dictionary<QuestName, QuestStatus>();

	private void Start()
	{ 
		// Adds each quest to the Disctionary, with a status of Inactive
		foreach(QuestName quest in Enum.GetValues(typeof(QuestName)))
		{
			Quests.Add(quest, QuestStatus.Inactive);
		}
	}

	public void SetQuestStatus(QuestName questName, QuestStatus questStatus)
	{
		Quests[questName] = questStatus;
	}
}
