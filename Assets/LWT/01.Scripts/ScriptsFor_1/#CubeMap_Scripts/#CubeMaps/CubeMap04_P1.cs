using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap04_P1 : CubeMap_P1
{
	[Header("Button Settings")]
	public Button instPreview_Btn;
	public Button groundInstall_Btn;
	public Button instClose_Btn;
	public Button groundClose_Btn;

	[Header("Canvas Settings")]
	public CV[] cvs;

	[Header("Text Setting")]
	public Text map4_Txt;

	private void Awake()
	{
		instPreview_Btn.enabled = true;
		groundInstall_Btn.enabled = false;

		instPreview_Btn.onClick.AddListener(OnClickInstPreviewBtn);
		groundInstall_Btn.onClick.AddListener(OnClickGroundIstBtn);
		instClose_Btn.onClick.AddListener(OnClickCloseBtn);
		groundClose_Btn.onClick.AddListener(OnClickCloseBtn);
	}

	private void OnEnable()
	{
		SetActiveCV(CVTag.Map4);
	}

	public void OnClickInstPreviewBtn()
	{
		Player.DisableAllScripts();

		CamManagement_P1.Instance.MoveTo(TargetTag_P1.Installation_Preview);

		groundInstall_Btn.enabled = true;

		map4_Txt.text = $"�õ��⸦ �ð��ϱ� �� ���� ���� �ٴ��� ������ Ȯ���� ���ڽ��ϴ�.\n\n�ٴ� �ð��ϱ� ��ư�� �������ּ���.";

		SetActiveCV(CVTag.InstPreview);
	}

	public void OnClickGroundIstBtn()
	{
		Player.DisableAllScripts();

		CamManagement_P1.Instance.MoveTo(TargetTag_P1.Installation_Preview);

		SetActiveCV(CVTag.GroundInstall);
	}

	public void OnClickCloseBtn()
	{
		Player.EnableScripts(new PlayerScriptTag[] { PlayerScriptTag.CamRotate });

		SetActiveCV(CVTag.Map4);
	}


	public void SetActiveCV(CVTag cvTag)
	{
		for (int i = 0; i < cvs.Length; i++)
		{
			if (cvs[i].tag == cvTag) cvs[i].gameObject.SetActive(true);
			else cvs[i].gameObject.SetActive(false);
		}
	}

	public enum CVTag { Map4, InstPreview, GroundInstall }
	[System.Serializable]
	public class CV
	{
		public string name;
		public CVTag tag;
		public GameObject gameObject;
	}
}
