using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 mos = Input.mousePosition;
			mos.z = Camera.main.farClipPlane; // 카메라가 보는 방향과, 시야를 가져온다.

			Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

			Ray ray = new Ray(Camera.main.transform.position, dir);
			RaycastHit hitInfo;

			int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

			if (Physics.Raycast(ray, out hitInfo, 100f, ~ignoreLayer))
			{
				if (hitInfo.collider.CompareTag("Button"))
				{
					hitInfo.collider.GetComponent<Button>().onClick.Invoke();
				}
			}
		}
	}
}
