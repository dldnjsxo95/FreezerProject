using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer_Stick : MonoBehaviour
{
	public Text numberTxt;
	public Transform rayPos;

	bool isChecked;
	float curTime = 0;

	public bool IsChecked
	{ get { return isChecked; } }

	public string CompareTagName { get; set; }

	private void OnEnable()
	{
		isChecked = false;
		numberTxt.text = "00.0";
		curTime = 0;
	}

	private void Update()
	{
		if (isChecked)
			return;

		Ray ray = new Ray(rayPos.position, rayPos.forward);

		RaycastHit hitInfo;

		int layerMask = 1 << LayerMask.NameToLayer("IgnoreLayer");

		if (Physics.Raycast(ray, out hitInfo, 100f, ~layerMask))
		{
			if (hitInfo.collider.CompareTag(CompareTagName))
			{
				switch (CompareTagName)
				{
					case "CoolingWater":
						curTime += Time.deltaTime;

						if (curTime > 2f)
						{
							numberTxt.text = Random.Range(28f, 33f).ToString("F1");
							isChecked = true;
						}
						break;
					case "EvaporatorWater":
						curTime += Time.deltaTime;

						if (curTime > 2f)
						{
							numberTxt.text = Random.Range(7f, 12f).ToString("F1");
							isChecked = true;
						}
						break;
				}
			}
		}
	}

}
