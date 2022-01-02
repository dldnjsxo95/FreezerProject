using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBool_P3 : MonoBehaviour
{
	public Animator animator;
	
    public string parameter;

    public void SetAnim(bool value)
	{
		animator.SetBool(parameter, value);
	}
}
