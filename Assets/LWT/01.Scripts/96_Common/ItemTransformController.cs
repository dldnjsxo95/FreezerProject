using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransformController : MonoBehaviour
{
	[Header("Size Setting")]
	[Range(1, 3)] public float sizeSensitivity = 1f;
	public float sMin = 2f;
	public float sMax = 4f;

	[Header("Rotate Setting")]
	[Range(0, 10)] public float rotateSensitivity = 1f;

	float sizeValue;

	private void Start()
	{
		sizeValue = transform.localScale.x;
	}

	private void Update()
	{
		// Size Controller
		sizeValue += Input.GetAxis("Mouse ScrollWheel") * sizeSensitivity ;
		sizeValue = Mathf.Clamp(sizeValue, sMin, sMax);

		transform.localScale = Vector3.one * sizeValue;

		// RotateController
		if(Input.GetMouseButton(0))
		{
			float mouseX = Input.GetAxisRaw("Mouse X") * rotateSensitivity;
			float mouseY = Input.GetAxisRaw("Mouse Y") * rotateSensitivity;

			transform.rotation *= Quaternion.AngleAxis(mouseX, -Vector3.up);
			transform.rotation *= Quaternion.AngleAxis(mouseY, Vector3.right);
		}
	}
}
