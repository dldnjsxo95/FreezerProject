using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTitlePanel_P2 : MonoBehaviour
{
	[Header("TextBoxes")]
	public PipeTextBox_P2 pipeTextBox;

	[Header("Panels")]
	public FadeObject pipePanel;
	public FadeObject brassPanel;
	public FadeObject silverPanel;

	[Header("Buttons")]
	public Button pipe_Btn;
	public Button brass_Btn;
	public Button silver_Btn;


	FadeObject trFadeObject;

	bool isActived;

	private void Awake()
	{
		trFadeObject = GetComponent<FadeObject>();

		pipe_Btn.onClick.AddListener(OnClickPipeBtn);
		brass_Btn.onClick.AddListener(OnClickBrassBtn);
		silver_Btn.onClick.AddListener(OnClickSilverBtn);
	}

	void OnClickPipeBtn()
	{
		trFadeObject.SetOffObject(0.5f);

		pipeTextBox.gameObject.SetActive(true);
	}
	void OnClickBrassBtn()
	{
		trFadeObject.SetOffObject(0.5f);

	}
	void OnClickSilverBtn()
	{
		trFadeObject.SetOffObject(0.5f);

	}
}
