using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PropBase : MonoBehaviour, IInteractable
{
	public Flowchart Flowchart { get; set; }
    public QuestController QuestController { get; set; }
	public TranslationManager TranslationManager { get; set; }
    public bool DisplayHints { get => _displayHints; set
		{
			if(value)
			{
                AddHintIcon();
			}
            else
			{
                RemoveHintIcon();
			}
		} 
    }

	protected GameManager gameManager;
    protected bool hasInteracted = false;
    protected abstract string FancyName { get; }

    private bool _displayHints;
    private GameObject hint;

    void Start()
    {
        Flowchart = GameObject.Find("PropsFlowchart").GetComponent<Flowchart>();
        QuestController = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestController>();
        TranslationManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<TranslationManager>();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        DisplayHints = gameManager.Settings.ShowHints;
    }

    private void AddHintIcon()
	{
        if (hint != null) return;
        hint = new GameObject("HintIcon");
        var parentScale = new Vector3(1/ transform.localScale.x, 1 / transform.localScale.y, 1/ transform.localScale.z ); 
        //get the inverse scaling since this child will naturally conform to the parent and needs to be normalized
        hint.transform.localScale = new Vector3(0.1f * parentScale.x, 0.1f * parentScale.y, 0.1f * parentScale.z); //based on parent scaling
        hint.transform.localPosition = new Vector3(0f, 0f, 0f);
        var image = hint.AddComponent<SpriteRenderer>();
        var texture = Resources.Load<Texture2D>("magnifier");
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sortingLayerName = "UI";
        hint.transform.SetParent(transform, false);
    }

    private void RemoveHintIcon()
	{
        Destroy(hint);
	}

    public virtual void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        gameManager.SetCursorAction(CursorAction.Question);
        gameManager.SetInteractDialogText(FancyName);
    }

    public virtual void OnMouseExit()
    {
        //Debug.Log("Mouse Exit");
        gameManager.SetCursorAction(CursorAction.Pointer);
        gameManager.SetInteractDialogActive(false);
    }

    /* Switched to OnMouseEnter
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
	{
        Debug.Log("Pointer Enter");
        gameManager.SetCursorAction(CursorAction.Question);
        gameManager.SetInteractDialogText(FancyName);
	}

    public virtual void OnPointerExit(PointerEventData pointerEventData)
	{
        Debug.Log("Pointer Exit");
        gameManager.SetCursorAction(CursorAction.Pointer);
        gameManager.SetInteractDialogActive(false);
    }*/

    /// <summary>
    ///     Default implementation of Interact. Override if you want to execute different code
    /// </summary>
    public virtual void Interact(ItemType item)
	{
		if (item == ItemType.NoItem)
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
