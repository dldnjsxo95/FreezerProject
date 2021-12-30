using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecificationPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	enum State { None, PointerDown, PointerHold, PointerUp }
	State state;

	Vector3 dist;
	FadeObject trFadeObject;

	[SerializeField]
	[Range(0, 10)]
	private float sensitivity = 1f; // Sensitivity Variable

	[SerializeField] public float zoomMin = 1f;
	[SerializeField] public float zoomMax = 3f;

	private void Awake()
	{
		trFadeObject = GetComponent<FadeObject>();
	}

	private void OnEnable()
	{
		trFadeObject.SetOnObject(0.5f);
	}

	private void Update()
	{
		Zoom();

		Drag();
	}

	private void Zoom()
	{
		float trSize = transform.localScale.x;


		trSize += Input.GetAxis("Mouse ScrollWheel") * sensitivity ;


		trSize = Mathf.Clamp(trSize, zoomMin, zoomMax);


		transform.localScale = Vector3.one * trSize;
	}



	private void Drag()
	{
		switch (state)
		{
			case State.PointerDown:
				dist = transform.position - Input.mousePosition;
				state = State.PointerHold;
				break;
			case State.PointerHold:
				transform.position = Input.mousePosition + dist;
				break;
			case State.PointerUp:
				state = State.None;
				break;
		}
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		state = State.PointerDown;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		state = State.PointerUp;
	}
}
