using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundInstall_CV_P1 : MonoBehaviour
{
	[Header("Table Setting")]
	public GameObject tableObjects;
	public ItemChooseManager_P1 itemManager;
	public Button ok_Btn;

	[Header("ItemCV Setting")]
	public Transform spawnedItemsHolder;
	public ItemCVs itemCVs;

	public static int state = (int)ItemTag_P1.Aligner;
	public static Transform SpawnedItemsHolder { get; set; }

	private void Awake()
	{
		tableObjects.SetActive(false);

		SpawnedItemsHolder = spawnedItemsHolder;
		itemCVs.gameObject.SetActive(true);
		itemCVs.SetOffAll();

		ok_Btn.onClick.AddListener(OnClickOkBtn);
	}

	private void Update()
	{
		if (tableObjects.activeInHierarchy)
		{
			if (state == (int)ItemTag_P1.Aligner) CubeMap04_P1.Map4_Txt.text = "테이블에 있는 수평자를 선택해 주세요.";
			if (state == (int)ItemTag_P1.Rubber) CubeMap04_P1.Map4_Txt.text = "테이블에 있는 방진고무를 선택해 주세요.";
			if (state == (int)ItemTag_P1.Spring) CubeMap04_P1.Map4_Txt.text = "테이블에 있는 스프링을 선택해 주세요.";
			if (state == (int)ItemTag_P1.Tape) CubeMap04_P1.Map4_Txt.text = "테이블에 있는 테이프를 선택해 주세요.";

			if (itemManager.ChoosenItem == null)
			{
				ok_Btn.interactable = false;
				return;
			}

			bool isStatg_Aligner = state == (int)ItemTag_P1.Aligner && itemManager.ChoosenItem.itemTag == ItemTag_P1.Aligner;
			bool isStatg_Rubber = state == (int)ItemTag_P1.Rubber && itemManager.ChoosenItem.itemTag == ItemTag_P1.Rubber;
			bool isStatg_Spring = state == (int)ItemTag_P1.Spring && itemManager.ChoosenItem.itemTag == ItemTag_P1.Spring;
			bool isStatg_Tape = state == (int)ItemTag_P1.Tape && itemManager.ChoosenItem.itemTag == ItemTag_P1.Tape;

			if (isStatg_Aligner || isStatg_Rubber || isStatg_Spring || isStatg_Tape) ok_Btn.interactable = true;
			else ok_Btn.interactable = false;
		}
	}

	public void OnClickOkBtn()
	{
		switch (itemManager.ChoosenItem.itemTag)
		{
			case ItemTag_P1.Aligner:
				ItemCVs.Init(itemCVs.aligner);
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

		CubeMap04_P1.GroundInstall_Btn.interactable = false;
		tableObjects.SetActive(false);
	}

	[System.Serializable]
	public class ItemCVs
	{
		public GameObject gameObject;

		[Header("ItemCVs")]
		public ItemCV_P1 aligner;
		public ItemCV_P1 rubber;
		public ItemCV_P1 spring;
		public ItemCV_P1 tape;

		public static void Init(ItemCV_P1 itemCV)
		{
			itemCV.gameObject.SetActive(true);
			itemCV.Init();
		}

		public void SetOffAll()
		{
			aligner.gameObject.SetActive(false);
			rubber.gameObject.SetActive(false);
			spring.gameObject.SetActive(false);
			tape.gameObject.SetActive(false);
		}

	}

}
