using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : PlayerScript
{
	float my;
	float mx;

	public enum MouseInput { Left, Right, Wheel }
	public MouseInput mouseInput;
	public enum State { process1, process2, process3 }
	public State state;

	[Range(0, 400)] public float rotSpeed = 200;

	private void OnEnable()
	{
		my = -transform.eulerAngles.x;
		mx = transform.eulerAngles.y;
	}

	private void Start()
	{
		my = -transform.eulerAngles.x;
		mx = transform.eulerAngles.y;
	}

	void Update()
	{
		if (!Input.GetMouseButton((int)mouseInput)) return;

		if (CamManagement.Instance != null && state == State.process3 && !CamManagement.Instance.IsFinishCamMove) return;
		else if (CamManagement_P1.Instance != null && state == State.process1 && !CamManagement_P1.Instance.IsFinishCamMove) return;

		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		my += mouseY * rotSpeed * Time.deltaTime;
		mx += mouseX * rotSpeed * Time.deltaTime;

		my = Mathf.Clamp(my, -70, 70);

		transform.eulerAngles = new Vector3(-my, mx, 0);

	}
}
