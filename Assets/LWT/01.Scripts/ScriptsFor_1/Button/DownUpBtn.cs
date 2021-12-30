using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownUpBtn : MonoBehaviour
{
	public FadeObject trFadeObject;
	public MoveObject mainText;
	public Text txt;
	bool isUp = true;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(OnClickBtn);
	}

	private void OnEnable()
	{
		trFadeObject.SetOnObject(0.5f);
	}

	public void OnClickBtn()
	{
		if (isUp)
		{
			mainText.MovingOut();
			txt.text = "�ø���";
		}
		else
		{
			mainText.MovingIn();
			txt.text = "������";
		}

		isUp = !isUp;
	}
}
