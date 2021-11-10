using Fungus;
using UnityEngine;

public abstract class NPCBase : NPCMovement, IInteractable
{
    public Flowchart Flowchart { get; set; }

    void Start()
    {
        Flowchart = GameObject.FindGameObjectWithTag("Flowchart").GetComponent<Flowchart>();
    }

    public abstract void Interact();
}
