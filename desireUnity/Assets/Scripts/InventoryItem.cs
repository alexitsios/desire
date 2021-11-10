using UnityEngine;

public class InventoryItem
{
	public ItemType _type;
	public Sprite _sprite;

    public InventoryItem(ItemType type, Sprite sprite)
	{
		_type = type;
		_sprite = sprite;
	}
}
