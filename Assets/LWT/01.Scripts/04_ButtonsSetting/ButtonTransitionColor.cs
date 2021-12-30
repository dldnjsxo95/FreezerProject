using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class ButtonTransitionColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerDownHandler, IPointerUpHandler
{
	public Color normalColor = Color.white;
	public Color disabledColor = Color.gray;
	public Color highlightedColor = new Color(1f, 1f, 136f / 255);
	public Color clickedColor = new Color(1f, 164f / 255, 143f / 255);

	Image[] images;
	Text[] texts;
	Button button;

	bool interactable = true;

	bool Interactable
	{
		get { return interactable; }
		set
		{
			if (interactable != value)
			{
				if (value)
				{
					images.ToList().ForEach(x => { Color color = normalColor; color.a = x.color.a; x.color = color; });
					texts.ToList().ForEach(x => { Color color = normalColor; color.a = x.color.a; x.color = color; });
				}
				else
				{
					images.ToList().ForEach(x => { Color color = disabledColor; color.a = x.color.a; x.color = color; });
					texts.ToList().ForEach(x => { Color color = disabledColor; color.a = x.color.a; x.color = color; });
				}
			}

			interactable = value;
		}
	}

	private void Awake()
	{
		images = transform.GetComponentsInChildren<Image>(LayerMask.NameToLayer("IgnoreColor"));
		texts = transform.GetComponentsInChildren<Text>(LayerMask.NameToLayer("IgnoreColor"));

		button = GetComponent<Button>();
	}

	private void Update()
	{
		Interactable = button.interactable;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!interactable)
			return;

		images.ToList().ForEach(x => { Color color = highlightedColor; color.a = x.color.a; x.color = color; });
		texts.ToList().ForEach(x => { Color color = highlightedColor; color.a = x.color.a; x.color = color; });
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!interactable)
			return;

		images.ToList().ForEach(x => { Color color = normalColor; color.a = x.color.a; x.color = color; });
		texts.ToList().ForEach(x => { Color color = normalColor; color.a = x.color.a; x.color = color; });
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!interactable)
			return;

		images.ToList().ForEach(x => { Color color = clickedColor; color.a = x.color.a; x.color = color; });
		texts.ToList().ForEach(x => { Color color = clickedColor; color.a = x.color.a; x.color = color; });
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!interactable)
			return;

		images.ToList().ForEach(x => { Color color = normalColor; color.a = x.color.a; x.color = color; });
		texts.ToList().ForEach(x => { Color color = normalColor; color.a = x.color.a; x.color = color; });
	}
}
