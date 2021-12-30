using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP2 : MonoBehaviour
{
	[Header("Canvas Holder")]
	public Transform canvasHolder;
	List<GameObject> canvases = new List<GameObject>();

	[Header("Canvas Setting")]
	public FadeObject titleFO;
	public FadeObject startFO;

	private void Awake()
	{
		for (int i = 0; i < canvasHolder.childCount; i++)
		{
			canvases.Add(canvasHolder.GetChild(i).gameObject);
		}
	}

	private void Start()
	{
		canvases.ForEach(x => x.gameObject.SetActive(false));

		StartCoroutine(StartRoutine());
	}

	IEnumerator StartRoutine()
	{
		titleFO.gameObject.SetActive(true);
		startFO.gameObject.SetActive(true);

		yield return new WaitForSeconds(1f);

		titleFO.SetOffObject(2f);
	}

}
