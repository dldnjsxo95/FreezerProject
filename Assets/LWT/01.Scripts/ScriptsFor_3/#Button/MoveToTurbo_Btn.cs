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
		// �����_2
		textBox_P3.textBox_Txt.text = "���� ���ô� �����(compressor)�� �õ��⸦ �����ϴ� ���¿����μ� �õ� ����Ŭ �� �øŸ� ��ȯ��Ű�� �߿��� ������ �մϴ�.";

		// ������� Mat�� �ٲ��ش�.
		compMatFadeEnable.gameObject.SetActive(true);
		compOutlineEnable.gameObject.SetActive(true);
		compBodyFadeEnable.gameObject.SetActive(true);
	}

	public void DisableNextBtn()
	{
		mainTxt_nextBtn.interactable = false;
	}

}
