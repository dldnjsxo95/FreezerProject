using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProcessCheck_P2 : MonoBehaviour
{
	public Process[] processes;

	private void Start()
	{
		for (int i = 0; i < processes.Length; i++)
		{
			processes[i].AddListener();
		}
	}

	private void OnEnable()
	{
		for (int i = 0; i < processes.Length; i++)
		{
			processes[i].InvokeEvent();
		}
	}

	[System.Serializable]
	public class Process
	{
		public string name;
		[Space(5)] public bool useButton = true;
		public Button button;
		public GameObject check_go;

		[Space(5)] public bool useEvent;
		public UnityEvent unityEvent;

		public void AddListener()
		{
			if (useButton) button.onClick.AddListener(SetCheckOn);
		}

		public void InvokeEvent()
		{
			if (useEvent) unityEvent.Invoke();
		}

		void SetCheckOn()
		{
			check_go.SetActive(true);
		}
	}
}
