using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class NPCBase : NPCMovement, IInteractable
{
    public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }

    protected GameManager gameManager;
    protected abstract string FancyName { get; }

    void Start()
    {
        Flowchart = GameObject.Find("NPCsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        gameManager.SetCursorAction(CursorAction.Dialog);
        gameManager.SetInteractDialogText(FancyName);
    }

    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        gameManager.SetCursorAction(CursorAction.Pointer);
        gameManager.SetInteractDialogActive(false);
    }

    public abstract void Interact(ItemType item);
}
