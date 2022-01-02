using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayComp_Btn : MonoBehaviour
{
	[Header("Button Setting")]
	public Button button;
	public Button mainTxt_nextBtn;

	[Header("Text Setting")]
	public TextBox_P3 textBox_P3;

	[Header("CompBtn Clicked Canvas")]
	public EnableController compBtn_CV;


	bool isClicked;

	private void Awake()
	{
		button.onClick.AddListener(OnClickBtn);
		mainTxt_nextBtn.interactable = false;
	}

	public void OnClickBtn()
	{
		mainTxt_nextBtn.interactable = true;
		this.gameObject.SetActive(false);

		// �����_2
		textBox_P3.textBox_Txt.text = "��� �µ��� ��Һ��� ���� ����� ���� �´ٰ� ���� ������ ���� ������ �ʿ��� ���Դϴ�.";

		compBtn_CV.gameObject.SetActive(true);
	}
}
