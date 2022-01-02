using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableController : MonoBehaviour
{
	public UnityEvent enableAction;
	public UnityEvent disableAction;

	public void OnEnable()
	{
		enableAction.Invoke();
	}

	public void OnDisable()
	{
		disableAction.Invoke();
	}
}
