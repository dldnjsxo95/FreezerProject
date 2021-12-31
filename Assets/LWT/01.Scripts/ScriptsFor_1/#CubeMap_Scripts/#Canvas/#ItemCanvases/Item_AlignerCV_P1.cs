using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_AlignerCV_P1 : ItemCV_P1
{
	[Header("Item Setting")]
	public Aligner aligner;

	[Header("Text Setting")]
	public Text percent_Txt;

	AlignerCheckBox_P1[] alignerCheckBox_P1s;

	bool isAllChecked;

	public override void Init()
	{
		aligner.gameObject.SetActive(true);
		aligner.SetPosPlayerHand();

		alignerCheckBox_P1s = GetComponentsInChildren<AlignerCheckBox_P1>();

		CubeMap04_P1.Map4_Txt.text = "�����ڷ� �ٴ��� ������ �´��� Ȯ�����ּ���.";
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if(!isAllChecked) CubeMap04_P1.Map4_Txt.text = "�ٴ��� ���콺�� �巡�����ּ���";

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

			if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, ~ignoreLayer))
			{
				if (hitInfo.collider.CompareTag("AlignerCheckBox"))
				{
					aligner.transform.position = hitInfo.point;
					aligner.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, -90));
					aligner.transform.localScale = Vector3.one * 400f;
				}
			}
		}

		int checkedCount = 0;

		for (int i = 0; i < alignerCheckBox_P1s.Length; i++)
		{
			if (alignerCheckBox_P1s[i].IsChecked) checkedCount++;
		}

		percent_Txt.text = Mathf.Ceil(100f / alignerCheckBox_P1s.Length * checkedCount).ToString() + "%";

		if (checkedCount == alignerCheckBox_P1s.Length)
		{
			CubeMap04_P1.GroundInstall_Btn.interactable = true;
			CubeMap04_P1.Map4_Txt.text = "������ ����ٸ� �õ��⸦ ��ġ �ϱ� �� ������� �ؾ� �մϴ�.\n\n'�ٴ� �ð��ϱ�'�� Ŭ�����ּ���.";
			isAllChecked = true;

			GroundInstall_CV_P1.state = (int)ItemTag_P1.Rubber;
		}
	}

	[System.Serializable]
	public class Aligner
	{
		public GameObject gameObject;
		public Vector3 startPos;
		public Vector3 rotation;
		public Vector3 size;

		public Transform transform
		{
			get { return gameObject.transform; }
		}

		public void SetPosPlayerHand()
		{
			gameObject.transform.localScale = startPos;
			gameObject.transform.localRotation = Quaternion.Euler(rotation);
			gameObject.transform.localScale = size;
		}
	}
}
