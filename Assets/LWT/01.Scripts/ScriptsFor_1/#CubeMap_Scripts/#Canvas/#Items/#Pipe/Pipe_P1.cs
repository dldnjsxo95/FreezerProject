using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_P1 : MonoBehaviour
{
	public enum State { Faded, Normal, ReadyToTapying, Taped }
	public State state;

	public bool IsClicked { get; set; } 
	public MeshRenderer MeshRenderer { get; set; }

	private void Awake()
	{
		MeshRenderer = GetComponent<MeshRenderer>();
	}

	public void SetReadyToTapying()
	{
		state = State.ReadyToTapying;
		IsClicked = false;
	}

}
