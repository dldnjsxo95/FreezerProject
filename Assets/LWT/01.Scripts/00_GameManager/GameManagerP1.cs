using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManagerP1 : MonoBehaviour
{
	[Header("Canvas Holder")]
	public Transform canvasHolder;
	List<GameObject> canvases = new List<GameObject>();

	[Header("Canvas Setting")]
	public FadeObject titleFO;

	[Header("Ohter Settings")]
	public GameObject refPref;
	public GameObject groundLine;
	public GameObject building; 

	public static event Action StartEvent;

	private void Awake()
	{
		for (int i = 0; i < canvasHolder.childCount; i++)
		{
			canvases.Add(canvasHolder.GetChild(i).gameObject);
		}

		StartEvent += () => canvases.ForEach(x => x.gameObject.SetActive(false));
		StartEvent += () => refPref.SetActive(false);
		StartEvent += () => groundLine.SetActive(false);
		StartEvent += () => StartCoroutine(StartRoutine());
		StartEvent += () => building.SetActive(false);
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
