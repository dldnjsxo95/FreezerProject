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

	Vector3 mPosDelta;
	Vector3 mPrevPos;

	private void Start()
	{
		sizeValue = transform.localScale.x;
	}

	private void Update()
	{
		// Size Controller
		sizeValue += Input.GetAxis("Mouse ScrollWheel") * sizeSensitivity;
		sizeValue = Mathf.Clamp(sizeValue, sMin, sMax);

		transform.localScale = Vector3.one * sizeValue;

		// RotateController
		if (Input.GetMouseButton(0))
		{
			mPosDelta = Input.mousePosition - mPrevPos;

			mPosDelta *= rotateSensitivity;

			if (Vector3.Dot(transform.up, Vector3.up) >= 0)
			{
				transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
			}
			else
			{
				transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
			}

			transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);

		}

		mPrevPos = Input.mousePosition;
	}
}
