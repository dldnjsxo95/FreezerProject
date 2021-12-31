using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTargets : MonoBehaviour
{
	public List<MaterialChanger> materialChangers = new List<MaterialChanger>();

	public void OnClick(int index)
	{
		if (index >= 4)
		{
			return;
		}
		materialChangers[index].Change();
	}
	int a = 0;
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnClick(a++);
		}
	}
}
