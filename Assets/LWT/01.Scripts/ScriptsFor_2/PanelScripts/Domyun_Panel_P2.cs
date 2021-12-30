using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domyun_Panel_P2 : MonoBehaviour
{
	public FadeObject nextPanel;
	public Button ok_Btn;

	FadeObject trFadeObject;

	bool isActived;

	private void Awake()
	{
		trFadeObject = GetComponent<FadeObject>();
	}

	private void Update()
	{
		if(ok_Btn != null && isActived == false)
		{
			ok_Btn.onClick.AddListener(OnClickNextBtn);
			isActived = true;
		}
	}


	void OnClickNextBtn()
	{
		trFadeObject.SetOffObject(0.5f);

		nextPanel.gameObject.SetActive(true);
		nextPanel.SetOnObject(0.5f);
	}


}
