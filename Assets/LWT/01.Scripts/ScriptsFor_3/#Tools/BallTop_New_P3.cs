using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTop_New_P3 : MonoBehaviour
{
	public TextBox_P3 textBox_P3;
	public GameObject ballTopPref;

	private void OnEnable()
	{
		textBox_P3.nextBtn.interactable = false;
		textBox_P3.prevBtn.interactable = false;
		ballTopPref.SetActive(false);
	}

	void Update()
	{
		if (!Input.GetMouseButtonDown(0)) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

		if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ~ignoreLayer))
		{
			if (!hitInfo.collider.CompareTag("Item")) return;

			if (hitInfo.collider.GetComponent<BallTop_New_P3>() == this)
			{
				textBox_P3.nextBtn.interactable = true;
				textBox_P3.prevBtn.interactable = true;
				textBox_P3.textBox_Txt.text = "다음 버튼을 눌러 이 볼탑을 교체해주세요.";

				ballTopPref.SetActive(true);
				this.gameObject.SetActive(false);
			}
		}
	}
}
