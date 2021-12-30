using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel_P2 : MonoBehaviour
{
	[Header("Buttons")]
	public Button ok_Btn;

	[Header("Panels")]
	public FadeObject nextPanel;

	[Header("Equipments")]
	public EquipmentImg_P2[] equipmentImg_Q2s;

	FadeObject trFadeObject;
	bool isActived;
	bool IsAllEquiped
	{
		get
		{
			int count = 0;

			foreach (EquipmentImg_P2 equip in equipmentImg_Q2s)
			{
				if (equip.IsEquiped)
					count++;
			}

			return count == equipmentImg_Q2s.Length ? true : false;
		}
	}

	private void Awake()
	{
		trFadeObject = GetComponent<FadeObject>();

		ok_Btn.interactable = false;

		StartCoroutine(StartRoutine());
	}

	private void Update()
	{
		if (IsAllEquiped) ok_Btn.interactable = true;
	}

	IEnumerator StartRoutine()
	{
		while (!isActived)
		{
			if (ok_Btn != null && isActived == false)
			{
				ok_Btn.onClick.AddListener(OnClickNextBtn);
				isActived = true;
			}

			yield return null;
		}
	}

	void OnClickNextBtn()
	{
		trFadeObject.SetOffObject(0.5f);

		nextPanel.gameObject.SetActive(true);
		nextPanel.SetOnObject(0.5f);
	}
}
