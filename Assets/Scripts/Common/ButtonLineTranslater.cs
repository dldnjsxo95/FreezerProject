using UnityEngine;
using UnityEngine.UI;


public class ButtonLineTranslater : MonoBehaviour
{
	RectTransform rt;

	public bool isVertical = true;

	[SerializeField]
	Transform origin;
	[SerializeField]
	Transform target;

	public void Awake()
	{
		if (rt == null)
		{
			rt = this.GetComponent<RectTransform>();
		}

		rt.pivot = isVertical ? new Vector2(0.5f, 1) : new Vector2(0, 0.5f);
	}

	float distance = 0;
	Vector3 size = Vector3.zero;

	public void LateUpdate()
	{
		if (origin == null || rt == null || target == null)
		{
			return;
		}

		distance = (origin.position - target.position).magnitude * 2;

		size = rt.sizeDelta;

		if (isVertical)
		{
			size.y = distance;
			rt.up = (origin.position - target.position).normalized;
		}
		else
		{
			size.x = distance;
			rt.right = (target.position - origin.position).normalized;
		}


		rt.sizeDelta = size;

		rt.position = origin.position;

	}


}
