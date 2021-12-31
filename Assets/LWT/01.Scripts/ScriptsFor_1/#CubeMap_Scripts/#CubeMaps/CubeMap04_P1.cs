using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class CubeMap04_P1 : CubeMap_P1
{
	[Header("Button Settings")]
	public Button instPreview_Btn;
	public Button groundInstall_Btn;
	public Button close_Btn;

	[Header("Ground Inst Setting")]
	public GroundInstall_CV_P1 groundInstall_CV_P1;

	[Header("Canvas Settings")]
	public CV[] cvs;

	[Header("Text Setting")]
	public Text map4_Txt;

	public static Button GroundInstall_Btn { get; set; }
	public static Text Map4_Txt { get; set; }

	private void Awake()
	{
		instPreview_Btn.enabled = true;
		groundInstall_Btn.interactable = false;

		instPreview_Btn.onClick.AddListener(OnClickInstPreviewBtn);
		groundInstall_Btn.onClick.AddListener(OnClickGroundIstBtn);
		close_Btn.onClick.AddListener(OnClickCloseBtn);

		GroundInstall_Btn = groundInstall_Btn;
		Map4_Txt = map4_Txt;

		Map4_Txt.text = "�ð��� �����ϱ� ���� ��ġ ���並 �����մϴ�.";
	}

	private void OnEnable()
	{
		SetOffAllCV();
	}

	public void OnClickInstPreviewBtn()
	{
		Player.DisableAllScripts();

		CamManagement_P1.Instance.MoveTo(TargetTag_P1.Installation_Preview);

		groundInstall_Btn.interactable = true;

		Map4_Txt.text = $"�ͺ� �õ��� �ð��� ���ؼ��� ��ġ�� ��Ҹ� �����Ͽ� �õ��⸦ ��ġ�ϱ⿡ �������� �����մϴ�";

		close_Btn.gameObject.SetActive(true);

		SetActiveCV(CVTag.InstPreview);
	}

	public void OnClickGroundIstBtn()
	{
		Player.DisableAllScripts();

		CamManagement_P1.Instance.MoveTo(TargetTag_P1.GroundView);

		SetActiveCV(CVTag.TableObjects);
	}

	public void OnClickCloseBtn()
	{
		Player.EnableScripts(new PlayerScriptTag[] { PlayerScriptTag.CamRotate });

		SetMainText();

		SetOffAllCV();
	}

	public void SetMainText()
	{
		switch (GroundInstall_CV_P1.state)
		{
			case (int)ItemTag_P1.Aligner:
				map4_Txt.text = "�õ��⸦ �ð��ϱ� �� ���� ���� �ٴ��� ������ Ȯ���� ���ڽ��ϴ�.\n\n'�ٴ� �ð��ϱ� ��ư'�� �������ּ���.";
				break;
			case (int)ItemTag_P1.Rubber:
				map4_Txt.text = "������ ����ٸ� �õ��⸦ ��ġ �ϱ� �� ������� �ؾ� �մϴ�.\n\n'�ٴ� �ð��ϱ�'�� Ŭ�����ּ���.";
				break;
			case (int)ItemTag_P1.Spring:
				map4_Txt.text = "���� �� ��ġ�� �Ϸ�Ǿ��ٸ�, ���� ������ ���� ��ġ�Ͽ� ���� �ð��� �������մϴ�.\n\n'�ٴ� �ð��ϱ�'�� Ŭ�����ּ���.";
				break;
			case (int)ItemTag_P1.Tape:
				map4_Txt.text = "���� ������ ��ġ�� �Ϸ�Ǿ��ٸ� õ��� �ͺ��õ��⸦ �����ϴ� õ�� �巡�� ������ ��ġ�غ��ڽ��ϴ�.\n\n'�ٴ� �ð��ϱ� ��ư'�� �������ּ���.";
				break;
		}

	}

	public void SetActiveCV(CVTag cvTag)
	{
		for (int i = 0; i < cvs.Length; i++)
		{
			if (cvs[i].tag == cvTag) cvs[i].gameObject.SetActive(true);
			else cvs[i].gameObject.SetActive(false);
		}
	}

	public void SetOffAllCV()
	{
		close_Btn.gameObject.SetActive(false);

		cvs.ToList().ForEach(x => x.gameObject.SetActive(false));
	}

	public enum CVTag { InstPreview, TableObjects, Aligner, Rubber, Spring, Tape }
	[System.Serializable]
	public class CV
	{
		public string name;
		public CVTag tag;
		public GameObject gameObject;
	}
}
