using EnumExtensions;
using System;
using System.Collections.Generic;
using TMPro;
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

		UpdateQuestTracker();
	}

	private void UpdateQuestTracker()
	{
		var text = "";

		foreach(var quest in Quests)
		{
			if(quest.Value == QuestStatus.Active)
			{
				text += $"- {quest.Key.GetFullName()}\n";
			}
		}

		GameObject.Find("QuestTrackerText").GetComponent<TMP_Text>().text = text;
	}
}
