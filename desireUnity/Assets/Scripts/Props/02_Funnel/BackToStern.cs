using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackToStern : PropBase
{
    protected override string FancyName { get { return "Go To Stern"; } }

	public override void OnPointerEnter(PointerEventData pointerEventData)
	{
		gameManager.SetCursorAction(CursorAction.RightArrow);
		gameManager.SetInteractDialogText(FancyName);
	}

	public override void Interact(ItemType item)
	{
		gameManager.LoadSceneAndSpawnPlayer(SceneName.Stern, 2);
	}
}
