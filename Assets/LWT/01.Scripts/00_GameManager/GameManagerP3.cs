using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP3 : MonoBehaviour
{
	[Header("Canvas Holder")]
	public Transform canvasHolder;
	List<GameObject> canvases = new List<GameObject>();

	[Header("Canvas Setting")]
	public FadeObject titleFO;
	public GameObject items_01;
	public TextBox_P3 mainText_01;


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
		mainText_01.gameObject.SetActive(true);

		yield return new WaitForSeconds(1f);

		items_01.SetActive(true);
		titleFO.SetOffObject(2f);
		mainText_01.moveObject.MovingIn();
	}

}
