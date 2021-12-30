using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel_P2 : MonoBehaviour
{
	[Header("Buttons")]
	public Button domyun_Btn;
	public Button YongJub_Btn;
	public Button next_Btn;

	[Header("Panels")]
	public FadeObject domyun_Paenl;
	public FadeObject yongJub_Paenl;
	public FadeObject safty_Paenl;

	FadeObject trFadeObject;
	bool isClickedDoMyun;
	bool isClickedYongJub;

	public bool IsBtnsClicked
	{
		get { return isClickedDoMyun && isClickedYongJub; }
	}

	private void Awake()
	{
		trFadeObject = GetComponent<FadeObject>();

		next_Btn.interactable = false;

		domyun_Btn.onClick.AddListener(OnClickDomyun);
		YongJub_Btn.onClick.AddListener(OnClickYongJub);
		next_Btn.onClick.AddListener(OnClickNext);
	}

	private void OnEnable()
	{
		if (IsBtnsClicked)
			next_Btn.interactable = true;
	}

	void OnClickDomyun()
	{
		isClickedDoMyun = true;

		domyun_Paenl.gameObject.SetActive(true);
		domyun_Paenl.SetOnObject(0.5f);

		trFadeObject.SetOffObject(0.5f);
	}
	
	void OnClickYongJub()
	{
		isClickedYongJub = true;

		yongJub_Paenl.gameObject.SetActive(true);
		yongJub_Paenl.SetOnObject(0.5f);

		trFadeObject.SetOffObject(0.5f);
	}

	void OnClickNext()
	{
		safty_Paenl.gameObject.SetActive(true);
		safty_Paenl.SetOnObject(0.5f);

		trFadeObject.SetOffObject(0.5f);
	}

}
