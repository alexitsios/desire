using Fungus;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IInteractable
{
    public ItemType itemType;
    public Sprite itemImage;

	public Flowchart Flowchart { get; set; }
	public QuestController QuestController { get; set; }

	protected PlayerInventory playerIventory;

	private void Start()
	{
		Flowchart = GameObject.Find("ItemsFlowchart").GetComponent<Flowchart>();
		QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();
		playerIventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}

	public abstract void Interact(ItemType item);
}
