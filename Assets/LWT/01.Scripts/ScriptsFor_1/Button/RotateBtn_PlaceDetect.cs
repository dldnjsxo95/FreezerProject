using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBtn_PlaceDetect : MonoBehaviour
{
	public FadeObject trFadeObject;
	bool isFoward = true;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(OnClickBtn);
	}

	private void OnEnable()
	{
		trFadeObject.SetOnObject(0.5f);
	}

	public void OnClickBtn()
	{
		//if (isFoward) CamManagement_P1.Instance.MoveTo(TargetTag_P1.PlaceDetect_2);
		//else CamManagement_P1.Instance.MoveTo(TargetTag_P1.PlaceDetect_1);

		isFoward = !isFoward;
	}

}
