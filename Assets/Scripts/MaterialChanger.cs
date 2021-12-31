using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
	public MeshRenderer mr;
	public Material material_origin;
	public Material material_change;

	bool b = false;

	public void Awake()
	{
		mr = GetComponent<MeshRenderer>();
	}



	public void Change()
	{
		mr.material = b ? material_origin : material_change;

		b = !b;
	}


	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			Change();
		}
	}

}
