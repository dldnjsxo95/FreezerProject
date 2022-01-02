using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangMat_P3 : MonoBehaviour
{
	[Header("MeshRenderer")]
    public MeshRenderer meshRenderer;

	[Header("Material")]
	public Material normalMat;
	public Material changeMat;

	[ContextMenu("SetNormal")]
    public void SetNormal()
	{
		meshRenderer.material = normalMat;
	}

	[ContextMenu("SetChange")]
	public void SetChange()
	{
		meshRenderer.material = changeMat;
	}

}
