using Fungus;

public interface IInteractable
{
	Flowchart Flowchart { get; set; }
	QuestController QuestController { get; set; }

	abstract void Interact(ItemType item);
}