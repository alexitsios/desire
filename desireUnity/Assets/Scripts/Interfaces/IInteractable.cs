using Fungus;

public interface IInteractable
{
	Flowchart Flowchart { get; set; }

	abstract void Interact();
}