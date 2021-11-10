using Fungus;
using UnityEngine;

public abstract class PropBase : MonoBehaviour, IInteractable
{
	public Flowchart Flowchart { get; set; }

	void Start()
    {
        Flowchart = GameObject.Find("PropsFlowchart").GetComponent<Flowchart>();
    }

    public abstract void Interact();
}
