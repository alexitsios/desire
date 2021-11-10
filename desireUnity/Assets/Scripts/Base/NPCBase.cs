using Fungus;
using UnityEngine;

public abstract class NPCBase : NPCMovement, IInteractable
{
    public Flowchart Flowchart { get; set; }

    void Start()
    {
        Flowchart = GameObject.Find("NPCsFlowchart").GetComponent<Flowchart>();
    }

    public abstract void Interact();
}
