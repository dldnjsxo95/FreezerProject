using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintButton : MonoBehaviour
{
	FadeObject trFadeObject;
	Button button;

	private void Start()
	{
		trFadeObject = GetComponent<FadeObject>();
		button = GetComponent<Button>();

		button.onClick.AddListener(OnClickPaintBtn);
	}

	public void OnClickPaintBtn()
	{
		button.interactable = false;
		trFadeObject.SetOffObject(0.5f);
	}
}
