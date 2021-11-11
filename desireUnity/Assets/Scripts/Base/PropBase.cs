using Fungus;
using UnityEngine;

public abstract class PropBase : MonoBehaviour, IInteractable
{
	public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }

    void Start()
    {
        Flowchart = GameObject.Find("PropsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();
    }

    public abstract void Interact(ItemType item);
}
