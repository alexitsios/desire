using Fungus;
using UnityEngine;

public abstract class NPCBase : NPCMovement, IInteractable
{
    public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }

    void Start()
    {
        Flowchart = GameObject.Find("NPCsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();
    }

    public abstract void Interact(ItemType item);
}
