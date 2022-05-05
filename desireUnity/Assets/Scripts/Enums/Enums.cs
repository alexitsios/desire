using System;

namespace EnumExtensions
{
	public static class EnumExtension
	{
		public static string GetFullName(this Enum value)
		{
			var type = value.GetType();
			var member = type.GetMember(value.ToString());
			return ((EnumName) member[0].GetCustomAttributes(typeof(EnumName), false)[0]).Name;
		}
	}
}

public enum CursorAction
{
	Pointer,
	LeftArrow,
	RightArrow,
	Question,
	Wait,
	Dialog,
	ItemSelected
}

/// <summary>
///		Enum to define each type of item. If another item is added in the future, it's type must be added to the list
/// </summary>
public enum ItemType
{
	NoItem,
	AllInOneTool,
	ServiceKit,
	MemoryComponent,
	EnergyCore,
	ClearanceCard,
	Keycard,
	DataPad,
	Monitor
}

/// <summary>
///     Defines the directions actors can move
/// </summary>
public enum Direction
{
	Top,
	Right,
	Bottom,
	Left
}

/// <summary>
///		Enum to define every quest in the game
/// </summary>
public enum QuestName
{
	[EnumName("Recover your missing arm")]
	RecoverArm,

	[EnumName("Recover your missing leg")]
	RecoverLeg,

	[EnumName("Open the door to reach the rest of the ship")]
	OpenSternDoor,

	[EnumName("Find a way to attach your missing arm")]
	CollectAllInOneTool,

	[EnumName("Find your master")]
	FindMaster,

	[EnumName("Get the clearance to embark on the life boat")]
	GetClearance,

	[EnumName("Recover your memory")]
	RecoverMemory
}

/// <summary>
///		Enum to define the status of a quest
/// </summary>
public enum QuestStatus
{
	Inactive,
	Active,
	Failed,
	Completed
}

public enum SceneName
{
	Stern = 1,
	Funnel,
	Superstructure_out,
	Superstructure_in,
	Generator_room,
	Bridge
}

public enum SettingsOptionType
{
	Combo,
	Slider
}

public enum Language
{
	English,
	Brazilian_Portuguese,
	Spanish,
	Greek
}