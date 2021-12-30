using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundLineCV : MonoBehaviour
{
    public FadeObject trFadeObject;

	[Header("Button Settings")]
	public bool nextBtnInteractable;
	public Button nextBtn;
	public Button groundBtn;

	[Header("Other Settings")]
	public Ruler ruler;
	public FadeObject rulerBox;
	public MainTextBox_P1 mainTextBox;

	private void Start()
	{
		groundBtn.onClick.AddListener(OnClickGroundBtn);
	}

	private void OnEnable()
	{
		trFadeObject.SetOnObject(0.5f);
		groundBtn.gameObject.SetActive(true);
		rulerBox.gameObject.SetActive(false);
	}

	private void Update()
	{
		if(ruler.IsChecked)
		{
			nextBtnInteractable = true;
			nextBtn.interactable = nextBtnInteractable;
			rulerBox.SetOffObject(0.5f);
			ruler.gameObject.SetActive(false);
			mainTextBox.eduTxt.text = "수평 확인이 완료되었습니다.";
		}
	}

	public void OnClickGroundBtn()
	{
		ruler.gameObject.SetActive(true);
		groundBtn.gameObject.SetActive(false);

		mainTextBox.eduTxt.text = "우선, 수평자를 이용해 현재 바닥의 수평을 확인 합니다.";

		Invoke("SetMainTextRuler", 2f);
	}

	public void SetButtonInteract()
	{
		nextBtn.interactable = nextBtnInteractable;
	}

	private void SetMainTextRuler()
	{
		mainTextBox.eduTxt.text = "마우스로 수평자를 옮겨 수평이 맞는곳을 찾아주세요.";

		rulerBox.gameObject.SetActive(true);
		rulerBox.SetOnObject(0.5f);
	}

}
