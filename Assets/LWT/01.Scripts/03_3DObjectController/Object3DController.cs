using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3DController : MonoBehaviour
{
    float zDist;

	private void Awake()
	{
		zDist = transform.localPosition.z;
	}

	void Update()
    {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, zDist));
    }
}
