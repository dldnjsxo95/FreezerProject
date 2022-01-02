using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManagerP1 : MonoBehaviour
{
	[Header("Canvas Setting")]
	public FadeObject titleFO;

	public static event Action StartEvent;

	private void Awake()
	{
		StartCoroutine(StartRoutine());
		StartEvent += () => Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
	}

	private void Start()
	{
		StartEvent?.Invoke();	
	}
	IEnumerator StartRoutine()
	{
		titleFO.gameObject.SetActive(true);

		yield return new WaitForSeconds(1f);

		titleFO.SetOffObject(2f);
	}

}
