using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaneAnimatorController : MonoBehaviour
{
	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void OpenVane()
	{
		Invoke("OpenAnim", 7f);
	}

	public void CloseVane()
	{
		animator.SetTrigger("Close");
	}

	void OpenAnim()
	{
		animator.SetTrigger("Open");
	}


}
