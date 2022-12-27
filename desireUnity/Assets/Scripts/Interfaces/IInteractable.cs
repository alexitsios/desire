using Fungus;
using UnityEngine.EventSystems;

public interface IInteractable// : IPointerEnterHandler, IPointerExitHandler
{
	Flowchart Flowchart { get; set; }
	QuestController QuestController { get; set; }

	abstract void Interact(ItemType item);
}