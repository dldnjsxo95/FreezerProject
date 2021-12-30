using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class WT_ChangeName : MonoBehaviour
{
	public enum State { Stop, Active }

	[Header("오브젝트의 이름 뒤에 _1 을 붙여 바꿔준다.")]
	public State state1;
	public string _name = "Quiz1";

	[Header("오브젝트의 이름을 모두 동일하게 바꿔준다.")]
	public State state2;
	public string _name2;

	[Header("오브젝트의 이름 뒤에 1을 붙여준다.")]
	public State state3;
	public string _name3;

	public GameObject[] go;


	void Update()
	{
		if (state1 == State.Active)
			Edit1();
		if (state2 == State.Active)
			Edit2();
		if (state3 == State.Active)
			Edit3();
	}

	void Edit1()
	{
		for (int i = 0; i < go.Length; i++)
		{
			go[i].transform.name = $"{_name}_{(i + 1).ToString()}";
		}

		state1 = State.Stop;
	}

	void Edit2()
	{
		for (int i = 0; i < go.Length; i++)
		{
			go[i].transform.name = _name2;
		}

		state2 = State.Stop;
	}

	private void Edit3()
	{
		for (int i = 0; i < go.Length; i++)
		{
			go[i].transform.name = $"{_name3}{(i + 1).ToString()}";
		}

		state3 = State.Stop;
	}
	
}
