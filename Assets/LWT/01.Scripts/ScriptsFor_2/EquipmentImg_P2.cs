using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentImg_P2 : BoxAdjustment , IPointerDownHandler, IPointerUpHandler
{
	enum State { None, PointerDown, PointerHold, PointerUp }
	State state;

	public GameObject check_Img;
	public bool IsEquiped { get; set; } = false;

	private void Update()
	{
		switch (state)
		{
			case State.PointerDown:
				transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, 10 );

				if (Vector3.Distance(transform.position, Input.mousePosition) < 10f)
					state = State.PointerHold;
				break;
			case State.PointerHold:
				transform.position = Input.mousePosition;
				break;
			case State.PointerUp:

				Collider[] colliders = Physics.OverlapBox(transform.position, boxSize);

				for(int i = 0; i < colliders.Length; i++)
				{
					if (colliders[i].CompareTag("Human"))
					{
						IsEquiped = true;
						check_Img.gameObject.SetActive(true);
						transform.gameObject.SetActive(false);
						break;
					}
				}

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
