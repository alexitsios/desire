using Fungus;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IInteractable
{
    public ItemType itemType;
    public Sprite itemImage;

	public Flowchart Flowchart { get; set; }

	private void Start()
	{
		Flowchart = GameObject.FindGameObjectWithTag("Flowchart").GetComponent<Flowchart>();
	}

	public abstract void Interact();
}
