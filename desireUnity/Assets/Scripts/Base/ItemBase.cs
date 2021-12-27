using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemBase : MonoBehaviour, IInteractable
{
    public ItemType itemType;
    public Sprite itemImage;

	public Flowchart Flowchart { get; set; }
	public QuestController QuestController { get; set; }

	protected PlayerInventory playerIventory;
	protected GameManager gameManager;
	protected abstract string FancyName { get; }

	private void Start()
	{
		Flowchart = GameObject.Find("ItemsFlowchart").GetComponent<Flowchart>();
		QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();

		playerIventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	public virtual void OnPointerEnter(PointerEventData pointerEventData)
	{
		gameManager.SetCursorAction(CursorAction.Question);
		gameManager.SetInteractDialogText(FancyName);
	}

	public virtual void OnPointerExit(PointerEventData pointerEventData)
	{
		gameManager.SetCursorAction(CursorAction.Pointer);
		gameManager.SetInteractDialogActive(false);
	}

	public abstract void Interact(ItemType item);
}
