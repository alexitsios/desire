using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class NPCBase : NPCMovement, IInteractable
{
    public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }
    public TranslationManager TranslationManager { get; set; }


    protected GameManager gameManager;
    protected abstract string FancyName { get; }

    void Start()
    {
        Flowchart = GameObject.Find("NPCsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();
        TranslationManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<TranslationManager>();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public virtual void OnMouseEnter()
    {
        gameManager.SetCursorAction(CursorAction.Dialog);
        gameManager.SetInteractDialogText(FancyName);
    }

    public virtual void OnMouseExit()
    {
        gameManager.SetCursorAction(CursorAction.Pointer);
        gameManager.SetInteractDialogActive(false);
    }

    /* Switched to OnMouseEnter
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        gameManager.SetCursorAction(CursorAction.Dialog);
        gameManager.SetInteractDialogText(FancyName);
    }

    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        gameManager.SetCursorAction(CursorAction.Pointer);
        gameManager.SetInteractDialogActive(false);
    }*/

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
