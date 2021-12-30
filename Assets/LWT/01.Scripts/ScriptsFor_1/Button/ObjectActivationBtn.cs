using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectActivationBtn : MonoBehaviour
{
	FadeObject trFadeObject;

	public bool activation = true;
    public GameObject gameObject;

	private void Awake()
	{
		gameObject.SetActive(false);
	}

	private void Start()
	{
		trFadeObject = GetComponent<FadeObject>();

		GetComponent<Button>().onClick.AddListener(OnClickBtn);
	}

	public void OnClickBtn()
	{
		gameObject.SetActive(activation);
		trFadeObject.SetOffObject(0.5f);
	}

}
