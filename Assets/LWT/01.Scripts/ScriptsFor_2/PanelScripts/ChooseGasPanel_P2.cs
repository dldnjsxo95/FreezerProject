using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChooseGasPanel_P2 : MonoBehaviour
{
	public PipeTextBox_P2 pipeTextBox;
	public OutLine[] outlineBtns;

	[Header("Button")]
	public Button nextBtn;

	public float perWaitTime = 0.1f;

	Coroutine setTextRoutine;

	private void Awake()
	{
		outlineBtns.ToList().ForEach(x => x.AddListener());
	}


	public void SetText()
	{
		if (setTextRoutine != null)
			StopCoroutine(setTextRoutine);

		if (gameObject.activeInHierarchy)
			setTextRoutine = StartCoroutine(SetTextRoutine());
	}

	IEnumerator SetTextRoutine()
	{
		pipeTextBox.eduTxt.text = "";
		outlineBtns.ToList().ForEach(x => x.gameObject.SetActive(false));
		yield return null;

		nextBtn.interactable = false;

		pipeTextBox.eduTxt.text = "1. ���� ������ ������ �ϱ� ���ؼ��� �Ƽ�ƿ���� ��Ұ� �ʿ��մϴ�.\n";
		yield return new WaitForSeconds(perWaitTime);

		pipeTextBox.eduTxt.text += "\n2. ��Ҵ� ��� ����̰�, �Ƽ�ƿ���� Ȳ�� ����Դϴ�.\n";
		yield return new WaitForSeconds(perWaitTime);

		pipeTextBox.eduTxt.text += "\n3. ��� ȣ������ ����̰�, �Ƽ�ƿ�� ȣ������ �������Դϴ�.";
		yield return new WaitForSeconds(1f);

		outlineBtns.ToList().ForEach(x => x.gameObject.SetActive(true));

		while (!IsAllClicked()) yield return null;

		nextBtn.interactable = true;
	}

	public bool IsAllClicked()
	{
		int count = 0;

		outlineBtns.ToList().ForEach(x => { if (!x.gameObject.activeInHierarchy) count++; });

		return count == outlineBtns.Length ? true : false;
	}

	[System.Serializable]
	public class OutLine
	{
		public Button button;
		[System.NonSerialized]
		public GameObject gameObject;

		public void AddListener()
		{
			gameObject = button.gameObject;

			button.onClick.AddListener(OnClickBtn);
		}

		public void OnClickBtn()
		{
			button.gameObject.SetActive(false);
		}
	}

}
