using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotateController : MonoBehaviour
{
	public CamManagement camManagement;

	private void OnEnable()
	{
		camManagement.IsRotate = true;
	}

	private void OnDisable()
	{
		camManagement.IsRotate = false;
	}
}
