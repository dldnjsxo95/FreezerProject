using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YongJub_Panel_P2 : MonoBehaviour
{
	public FadeObject nextPanel;
	public Button ok_Btn;

	FadeObject trFadeObject;

	bool isActived;

	private void Awake()
	{
		trFadeObject = GetComponent<FadeObject>();

		StartCoroutine(UpdateRoutine());
	}

	IEnumerator UpdateRoutine()
	{
		while(!isActived)
		{
			if (ok_Btn != null && isActived == false)
			{
				ok_Btn.onClick.AddListener(OnClickNextBtn);
				isActived = true;
			}

			yield return null;
		}
	}

	void OnClickNextBtn()
	{
		trFadeObject.SetOffObject(0.5f);

		nextPanel.gameObject.SetActive(true);
		nextPanel.SetOnObject(0.5f);
	}
}
