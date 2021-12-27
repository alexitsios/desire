using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PropBase : MonoBehaviour, IInteractable
{
	public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }

    protected GameManager gameManager;
    protected abstract string FancyName { get; }

    void Start()
    {
        Flowchart = GameObject.Find("PropsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();

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

    /// <summary>
    ///     Default implementation of Interact. Override if you want to execute different code
    /// </summary>
    public virtual void Interact(ItemType item)
	{
        if(item == ItemType.NoItem)
		{
            var blockName = gameObject.name;
            Flowchart.ExecuteBlock(blockName);
		}
        else
		{
            Flowchart.ExecuteBlock("ItemUseError");
		}
	}
}
