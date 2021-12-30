using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DomyunCheck_P2 : MonoBehaviour
{
	public Button button1;
	public Button button2;
	public GameObject check_go;

	bool isClicked1;
	bool isClicked2;

	public void OnClickBtn1() => isClicked1 = true;
	public void OnClickBtn2() => isClicked2 = true;

	Coroutine invokeRoutine;

	private void Start()
	{
		button1.onClick.AddListener(OnClickBtn1);
		button2.onClick.AddListener(OnClickBtn2);
	}

	public void Invoke()
	{
		if (invokeRoutine != null) StopCoroutine(invokeRoutine);

		invokeRoutine = StartCoroutine(CheckRoutine());
	}

	IEnumerator CheckRoutine()
	{
		while (!isClicked1 || !isClicked2)
			yield return null;

		check_go.SetActive(true);
	}

}
