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
	RecoverArm,
	RecoverLeg,
	OpenSternDoor,
	CollectAllInOneTool,
	FindMaster
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
	Settings = 1,
	Inventory = 2,
	Stern = 3,
	Funnel = 4,
	Superstructure_out = 5
}

public enum MenuOptionType
{
	Dropdown,
	Select,
	Button
}

public enum Language
{
	English,
	Brazilian_Portuguese,
	Spanish,
	Greek
}