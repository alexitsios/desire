using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class NPCBase : NPCMovement, IInteractable
{
    public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }

    protected GameManager gameManager; 

    void Start()
    {
        Flowchart = GameObject.Find("NPCsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        gameManager.SetCursorAction(CursorAction.Dialog);
    }

    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        gameManager.SetCursorAction(CursorAction.Pointer);
    }

    public abstract void Interact(ItemType item);
}
