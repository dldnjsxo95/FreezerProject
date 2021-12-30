using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ItemTag_P1 { Aligner, Rubber, Spring, Tape, }

public class Item_P1 : MonoBehaviour
{
	public ItemTag_P1 itemTag;
	Outline[] outline = new Outline[0];

	public Outline[] Outlines
	{
		get { return outline; }
	}

	private void Awake()
	{
		outline = GetComponentsInChildren<Outline>(true);
	}

	public void EnAbleOutLine()
	{
		outline.ToList().ForEach(x => x.enabled = true);
	}

	public void DisAbleOutLine()
	{
		outline.ToList().ForEach(x => x.enabled = false);
	}

}
