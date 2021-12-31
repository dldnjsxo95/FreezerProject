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

		Map4_Txt.text = "시공을 진행하기 위해 설치 검토를 진행합니다.";
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

		Map4_Txt.text = $"터보 냉동기 시공을 위해서는 설치할 장소를 검토하여 냉동기를 설치하기에 적절한지 검토합니다";

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
				map4_Txt.text = "냉동기를 시공하기 전 가장 먼저 바닥의 수평을 확인해 보겠습니다.\n\n'바닥 시공하기 버튼'을 선택해주세요.";
				break;
			case (int)ItemTag_P1.Rubber:
				map4_Txt.text = "수평이 맞췄다면 냉동기를 설치 하기 전 방진대비를 해야 합니다.\n\n'바닥 시공하기'를 클릭해주세요.";
				break;
			case (int)ItemTag_P1.Spring:
				map4_Txt.text = "방진 고무 설치가 완료되었다면, 방진 스프링 까지 설치하여 방진 시공을 마무리합니다.\n\n'바닥 시공하기'를 클릭해주세요.";
				break;
			case (int)ItemTag_P1.Tape:
				map4_Txt.text = "방진 스프링 설치가 완료되었다면 천장과 터보냉동기를 연결하는 천장 드래드 라인을 설치해보겠습니다.\n\n'바닥 시공하기 버튼'을 선택해주세요.";
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
