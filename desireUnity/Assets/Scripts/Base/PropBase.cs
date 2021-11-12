using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PropBase : MonoBehaviour, IInteractable
{
	public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }

    protected GameManager gameManager;

    void Start()
    {
        Flowchart = GameObject.Find("PropsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public virtual void OnPointerEnter(PointerEventData pointerEventData)
	{
        gameManager.SetCursorAction(CursorAction.Question);
	}

    public virtual void OnPointerExit(PointerEventData pointerEventData)
	{
        gameManager.SetCursorAction(CursorAction.Pointer);
    }

    /// <summary>
    ///     Default implementation of Interact. Override if you want to execute different code
    /// </summary>
    public virtual void Interact(ItemType item)
	{
        var blockName = gameObject.name;
        Flowchart.ExecuteBlock(blockName);
	}
}
