using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : MonoBehaviour
{
	bool isChecked;
	float curTime = 0;

	public bool IsChecked
	{ get { return isChecked; } }

	private void OnEnable()
	{
		isChecked = false;
		curTime = 0;
	}

	private void Update()
	{
		if (isChecked)
			return;

		Vector3 mos = Input.mousePosition;
		mos.z = Camera.main.farClipPlane; // 카메라가 보는 방향과, 시야를 가져온다.

		Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

		Ray ray = new Ray(Camera.main.transform.position, dir);

		RaycastHit hitInfo;

		int layerMask = 1 << LayerMask.NameToLayer("IgnoreLayer");

		if (Physics.Raycast(ray, out hitInfo, 100f, ~layerMask))
		{
			transform.position = hitInfo.point;

			if (hitInfo.collider.CompareTag("RulerCheckBox"))
			{
				curTime += Time.deltaTime;

				if (curTime > 2f) isChecked = true;
			}
		}
	}
}
