using UnityEngine;

public class MovementBase : MonoBehaviour
{
	public Vector3 minPosition = new Vector3(0, 2.25f, 0);
	public Vector3 minScale = new Vector3(5, 5, 1);

	public Vector3 maxPosition = new Vector3(0, -2.85f, 0);
	public Vector3 maxScale = new Vector3(7, 7, 1);

	public void UpdateSizeForDepth(SpriteRenderer spriteRenderer)
	{
		float scaleValue = Mathf.Lerp(minScale.y, maxScale.y, Mathf.InverseLerp(minPosition.y, maxPosition.y, transform.position.y));
		transform.localScale = new Vector3(scaleValue, scaleValue, 0);

		spriteRenderer.sortingOrder = (int) (transform.position.y * -1);
	}
}
