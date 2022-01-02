using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTop_Delete : MonoBehaviour
{
	public TextBox_P3 textBox_P3;

	public void OnEnable()
	{
		textBox_P3.nextBtn.interactable = false;
	}

	void Update()
    {
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			int ignoreLayer = 1 << LayerMask.NameToLayer("ignoreLayer");

			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ~ignoreLayer))
			{
				if(hitInfo.collider.CompareTag("BallTop_Old"))
				{
					if(hitInfo.collider.GetComponent<BallTop_Delete>() == this)
					{
						hitInfo.collider.gameObject.SetActive(false);
						textBox_P3.nextBtn.interactable = true;
					}
				}
			}
		}
	}
}
