using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CheckPaintCV : MonoBehaviour
{
	public FadeObject trFadeObject;

	[Header("Button Settings")]
	public bool nextBtnInteractable;
	public Button[] textBtns;

	[Header("RandomPos Holder")]
	public Transform paint_Holder;
	public FadeObject[] paints;

	List<FadeObject> onPaints = new List<FadeObject>();

	public bool IsChecked { get; set; } = false;

	private void Awake()
	{
		SetPaints();
	}

	private void OnEnable()
	{
		trFadeObject.SetOnObject(0.5f);
	}

	private void Update()
	{
		if (IsChecked) return;

		int count = 0;

		for (int i = 0; i < paints.Length; i++)
		{
			if (!paints[i].gameObject.activeInHierarchy) count++;
		}

		if (count == paints.Length)
		{ 
			IsChecked = true;
			nextBtnInteractable = true;
			textBtns.ToList().ForEach(x => x.interactable = nextBtnInteractable);
		}
	}

	private void SetPaints()
	{
		List<FadeObject> paintFos = paints.ToList();

		paintFos.ForEach(x => x.gameObject.SetActive(false));

		for (int i = 0; i < 5; i++)
		{
			int randNum = Random.Range(0, paintFos.Count);

			onPaints.Add(paintFos[randNum]);

			paintFos.RemoveAt(randNum);
			onPaints[i].gameObject.SetActive(true);
		}
	}

	[ContextMenu("SetPaint")]
	public void SetPaint()
	{
		paints = paint_Holder.GetComponentsInChildren<FadeObject>();
	}

	public void SetButtonInteract()
	{
		textBtns.ToList().ForEach(x => x.interactable = nextBtnInteractable);
	}
}
