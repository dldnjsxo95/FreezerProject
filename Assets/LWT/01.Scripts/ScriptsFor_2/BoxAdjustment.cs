using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxAdjustment : MonoBehaviour
{
	CanvasScaler cs;
	RectTransform rt;
	float screenRatio;

	public float rtWidth = 2f;
	protected Vector3 boxSize;

	private void Awake()
	{
		cs = GameObject.FindWithTag("MainCV").GetComponent<CanvasScaler>();
		rt = GetComponent<RectTransform>();

		if (cs.referenceResolution.x / cs.referenceResolution.y < Screen.width / Screen.height)
			screenRatio = Screen.height / cs.referenceResolution.y;
		else
			screenRatio = Screen.width / cs.referenceResolution.x;

		boxSize = new Vector3(rt.sizeDelta.x * screenRatio * transform.localScale.x, rt.sizeDelta.y * screenRatio * transform.localScale.y, rtWidth);
		boxSize *= 0.5f; // Overlap Box의 조건을 만족 시키기 위해

	}

}
