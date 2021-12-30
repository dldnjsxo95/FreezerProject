using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
	public enum State { OutPos, InPos}
	public State state;

	public Vector3 inPos;
	public Vector3 outPos;

	private void Awake()
	{
		if(state == State.OutPos) transform.localPosition = outPos;
		else if(state == State.InPos) transform.localPosition = inPos;
	}

	public void MovingIn()
	{
		DOTween.Kill(transform.gameObject);

		transform.DOLocalMove(inPos,1f);
	}

	public void MovingOut()
	{
		DOTween.Kill(transform.gameObject);

		transform.DOLocalMove(outPos, 1f);

	}

	void SetOff()
	{
		gameObject.SetActive(false);
	}
}
