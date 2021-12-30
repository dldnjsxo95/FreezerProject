using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMap01_P1 : CubeMap_P1
{
	public TextBox_P1 mainTextBox;

	private void Awake()
	{
		Invoke("MovingIn", 1.5f);
	}

	public void MovingIn() => mainTextBox.moveObject.MovingIn();
}
