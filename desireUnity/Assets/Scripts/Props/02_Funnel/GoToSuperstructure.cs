using UnityEngine.EventSystems;

public class GoToSuperstructure : PropBase
{
	protected override string FancyName { get { return "Go to Superstructure Access"; } }

	public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		gameManager.SetCursorAction(CursorAction.LeftArrow);
		gameManager.SetInteractDialogText(FancyName);
	}

	public override void Interact(ItemType item)
	{
		gameManager.LoadSceneAndSpawnPlayer(SceneName.Superstructure_out, 1);
	}
}
