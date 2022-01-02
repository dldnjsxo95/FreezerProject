using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTop_New_Hand_P3 : MonoBehaviour
{
	public TextBox_P3 textBox_P3;
	public GameObject ballTop_Old;
	public EnableController fillFull;
	public EnableController fullOut;
	float zDist;

	private void Awake()
	{
		zDist = transform.localPosition.z;
	}

	void Update()
	{
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, zDist));

		if (!Input.GetMouseButtonDown(0)) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

		if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ~ignoreLayer))
		{
			if (!hitInfo.collider.CompareTag("BallTop_Box")) return;

			ballTop_Old.gameObject.SetActive(true);

			fillFull.gameObject.SetActive(false);
			fullOut.gameObject.SetActive(true);

			textBox_P3.nextBtn.interactable = true;

			this.gameObject.SetActive(false);
		}

	}
}
