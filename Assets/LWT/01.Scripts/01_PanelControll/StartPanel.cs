using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
	[Header("Button_Setting")]
	public Button[] buttons = new Button[2];

	[Header("FirstButton Setting")]
	public FadeObject domyunFO;

	[Header("SecondButton Setting")]
	public FadeObject textBox;

	FadeObject trFO;

	private void Awake()
	{
		trFO = GetComponent<FadeObject>();

		buttons[0].onClick.AddListener(OnClickFirstBtn);
		buttons[1].onClick.AddListener(OnClickSecondBtn);
	}

	private void OnEnable()
	{
		buttons[0].gameObject.SetActive(true);
		buttons[1].gameObject.SetActive(false);
	}

	public void OnClickFirstBtn()
	{
		domyunFO.gameObject.SetActive(true);
		domyunFO.SetOnObject(0.5f);

		buttons[0].gameObject.SetActive(false);
		buttons[1].gameObject.SetActive(true);
	}

	public void OnClickSecondBtn()
	{
		domyunFO.SetOffObject(0.5f);
		trFO.SetOffObject(0.5f);

		textBox.gameObject.SetActive(true);
	}
}
