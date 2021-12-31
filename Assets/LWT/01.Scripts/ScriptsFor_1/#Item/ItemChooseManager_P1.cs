using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemChooseManager_P1 : MonoBehaviour
{
	public Item_P1[] item_P1s;

	public Item_P1 ChoosenItem { get; set; } = null;

	private void OnEnable()
	{
		ChoosenItem = null;
		DisAbleAllOutLine();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

			if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, ~ignoreLayer))
			{
				if (hitInfo.collider.CompareTag("Item"))
				{
					ChoosenItem = hitInfo.collider.GetComponent<Item_P1>();
					EnAbleOutLine(ChoosenItem);
				}
			}
		}
	}

	private void EnAbleOutLine(Item_P1 item)
	{
		foreach (Item_P1 item_P1 in item_P1s)
		{
			if (item_P1 == item) item_P1.EnAbleOutLine();
			else item_P1.DisAbleOutLine();
		}
	}

	private void DisAbleAllOutLine()
	{
		item_P1s.ToList().ForEach(x => { if (x.Outlines.Length != 0) x.DisAbleOutLine(); });
	}

}
