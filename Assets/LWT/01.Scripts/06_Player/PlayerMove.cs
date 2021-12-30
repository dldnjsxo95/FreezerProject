using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerScript
{
	[Range(0, 10), SerializeField] float moveSpeed = 5f;
	CharacterController cc;

	private void Awake()
	{
		cc = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		Vector3 dir = transform.right * x + transform.forward * y;
		
		dir.y = 0;

		cc.Move(moveSpeed * dir.normalized * Time.deltaTime);
	}
}
