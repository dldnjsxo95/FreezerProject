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
			mainTextBox.eduTxt.text = "���� Ȯ���� �Ϸ�Ǿ����ϴ�.";
		}
	}

	public void OnClickGroundBtn()
	{
		ruler.gameObject.SetActive(true);
		groundBtn.gameObject.SetActive(false);

		mainTextBox.eduTxt.text = "�켱, �����ڸ� �̿��� ���� �ٴ��� ������ Ȯ�� �մϴ�.";

		Invoke("SetMainTextRuler", 2f);
	}

	public void SetButtonInteract()
	{
		nextBtn.interactable = nextBtnInteractable;
	}

	private void SetMainTextRuler()
	{
		mainTextBox.eduTxt.text = "���콺�� �����ڸ� �Ű� ������ �´°��� ã���ּ���.";

		rulerBox.gameObject.SetActive(true);
		rulerBox.SetOnObject(0.5f);
	}

}
