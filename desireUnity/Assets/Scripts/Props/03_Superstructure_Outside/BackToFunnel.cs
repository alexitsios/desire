using UnityEngine.EventSystems;

public class BackToFunnel : PropBase
{
    protected override string FancyName { get { return "Go to Funnel"; } }

	public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		gameManager.SetCursorAction(CursorAction.RightArrow);
		gameManager.SetInteractDialogText(FancyName);
	}

	public override void Interact(ItemType item)
	{
		gameManager.LoadSceneAndSpawnPlayer(SceneName.Funnel, 2);
	}
}
