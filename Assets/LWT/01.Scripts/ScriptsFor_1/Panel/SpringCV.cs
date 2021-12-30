using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpringCV : MonoBehaviour
{
	public FadeObject trFadeObject;

	[Header("Button Settings")]
	public bool nextBtnInteractable;
	public Button nextBtn;
	public ObjectActivationBtn[] onBtn;

	public bool IsChecked { get; set; } = false;

	private void OnEnable()
	{
		trFadeObject.SetOnObject(0.5f);
	}

	private void Update()
	{
		if (IsChecked) return;

		int count = 0;

		for (int i = 0; i < onBtn.Length; i++)
		{
			if (onBtn[i].gameObject.activeInHierarchy) count++;
		}

		if (count == onBtn.Length)
		{
			IsChecked = true;
			nextBtnInteractable = true;
			nextBtn.interactable = nextBtnInteractable;
		}
	}

	public void SetButtonInteract()
	{
		nextBtn.interactable = nextBtnInteractable;
	}
}
