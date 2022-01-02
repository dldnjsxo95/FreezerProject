using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_P3 : MonoBehaviour
{
	public Animator animator;

	public string play = "Play";
	public string stop = "Stop";

	public void Play()
	{
		animator.SetTrigger(play);
	}

	public void Stop()
	{
		animator.SetTrigger(stop);
	}
}
