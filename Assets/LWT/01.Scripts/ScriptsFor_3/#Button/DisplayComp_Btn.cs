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

		// 압축기_2
		textBox_P3.textBox_Txt.text = "베어링 온도가 평소보다 낮고 압축기 토출 온다고 낮은 것으로 보아 점검이 필요해 보입니다.";

		compBtn_CV.gameObject.SetActive(true);
	}
}
