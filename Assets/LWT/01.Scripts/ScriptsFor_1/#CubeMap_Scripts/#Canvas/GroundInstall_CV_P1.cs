using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundInstall_CV_P1 : MonoBehaviour
{
	[Header("Table Setting")]
	public ItemChooseManager_P1 itemManager;
	public Text groundTextBox_Txt;
	public Button ok_Btn;

	[Header("ItemCV Setting")]
	public ItemCVs itemCVs;

	public static int stage = (int)ItemTag_P1.Aligner;

	private void Awake()
	{
		groundTextBox_Txt.text = "테이블에 있는 수평자를 선택해 주세요.";

		ok_Btn.onClick.AddListener(OnClickOkBtn);
	}

	private void OnEnable()
	{
		ok_Btn.interactable = false;
	}

	private void Update()
	{
		if (itemManager.ChoosenItem == null) return;

		bool isStatg_Aligner = stage == (int)ItemTag_P1.Aligner && itemManager.ChoosenItem.itemTag == ItemTag_P1.Aligner;
		bool isStatg_Rubber = stage == (int)ItemTag_P1.Rubber && itemManager.ChoosenItem.itemTag == ItemTag_P1.Rubber;
		bool isStatg_Spring = stage == (int)ItemTag_P1.Spring && itemManager.ChoosenItem.itemTag == ItemTag_P1.Spring;
		bool isStatg_Tape = stage == (int)ItemTag_P1.Tape && itemManager.ChoosenItem.itemTag == ItemTag_P1.Tape;

		if (isStatg_Aligner || isStatg_Rubber || isStatg_Spring || isStatg_Tape) ok_Btn.interactable = true;
		else ok_Btn.interactable = false;
	}

	public void OnClickOkBtn()
	{
		switch (itemManager.ChoosenItem.itemTag)
		{
			case ItemTag_P1.Aligner:
				ItemCVs.Init(itemCVs.alligner);
				break;
			case ItemTag_P1.Rubber:
				ItemCVs.Init(itemCVs.rubber);
				break;
			case ItemTag_P1.Spring:
				ItemCVs.Init(itemCVs.spring);
				break;
			case ItemTag_P1.Tape:
				ItemCVs.Init(itemCVs.tape);
				break;
		}

		this.gameObject.SetActive(false);
	}

	[System.Serializable]
	public class ItemCVs
	{
		public ItemCV_P1 alligner;
		public ItemCV_P1 rubber;
		public ItemCV_P1 spring;
		public ItemCV_P1 tape;

		public static void Init(ItemCV_P1 itemCV)
		{
			itemCV.gameObject.SetActive(true);
			itemCV.Init();
		}

	}

}
