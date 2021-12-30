using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel_P2 : MonoBehaviour
{
	public FadeObject menuPanel;
    public Button startBtn;


	FadeObject trFadeObject;

	private void Awake()
	{
		trFadeObject = GetComponent<FadeObject>();

		startBtn.onClick.AddListener(OnClickStudyBtn);
	}

	public void OnClickStudyBtn()
	{
		menuPanel.gameObject.SetActive(true);
		menuPanel.SetOnObject(0.5f);

		trFadeObject.SetOffObject(0.5f);
	}


}
