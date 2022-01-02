using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToTurbo_Btn : MonoBehaviour
{
	[Header("Button Setting")]
	public Button button;
	public Button mainTxt_nextBtn;

	[Header("Text Setting")]
	public TextBox_P3 textBox_P3;

	[Header("ChangeMat Setting")]
	public EnableController compMatFadeEnable;
	public EnableController compOutlineEnable;
	public EnableController compBodyFadeEnable;

	bool isClicked;

	private void Awake()
	{
		button.onClick.AddListener(OnClickBtn);
	}

	public void OnClickBtn()
	{
		mainTxt_nextBtn.interactable = true;
		this.gameObject.SetActive(false);

		CamManagement.Instance.MoveTo(TargetTag.MoveToTurbo);
		// 압축기_2
		textBox_P3.textBox_Txt.text = "지급 보시는 압축기(compressor)는 냉동기를 구동하는 동력원으로서 냉동 사이클 내 냉매를 순환시키는 중요한 역할을 합니다.";

		// 압축기의 Mat을 바꿔준다.
		compMatFadeEnable.gameObject.SetActive(true);
		compOutlineEnable.gameObject.SetActive(true);
		compBodyFadeEnable.gameObject.SetActive(true);
	}

	public void DisableNextBtn()
	{
		mainTxt_nextBtn.interactable = false;
	}

}
