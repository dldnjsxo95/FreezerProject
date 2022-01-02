using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraCV : MonoBehaviour
{
	private void Update()
	{
		transform.forward = transform.position - Camera.main.transform.position;
	}
}
