using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class Item_RubberCV_P1 : ItemCV_P1
{
	[Header("Item Setting")]
	public Rubber rubber;

	[Header("Button Setting")]
	public RubberButton[] rubberButtons;

	public override void Init()
	{
		rubber.gameObject.SetActive(true);
		rubber.SetPosPlayerHand();

		rubberButtons.ToList().ForEach(x =>
		{
			x.button.onClick.AddListener(x.OnClickButton);
			x.button.onClick.AddListener(() => OnClickRubberBtn(x.button.transform.position));
		});

		CubeMap04_P1.Map4_Txt.text = "���� ���� �ٴڿ� ��ġ�� �ּ���.";
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

		if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, ~ignoreLayer))
		{
			if (hitInfo.collider.CompareTag("AlignerCheckBox"))
			{
				rubber.transform.position = hitInfo.point;
				rubber.transform.localRotation = Quaternion.Euler(new Vector3(-180, 0, 0));
				rubber.transform.localScale = Vector3.one * 400f;
			}
			else if (hitInfo.collider.CompareTag("Button"))
			{
				rubber.transform.position = hitInfo.transform.position;
			}
		}

		int clickedCount = 0;

		for (int i = 0; i < rubberButtons.Length; i++)
		{
			if (rubberButtons[i].isClicked) clickedCount++;
		}

		if(clickedCount == rubberButtons.Length)
		{
			CubeMap04_P1.GroundInstall_Btn.interactable = true;
			CubeMap04_P1.Map4_Txt.text = "���� �� ��ġ�� �Ϸ�Ǿ��ٸ�, ���� ������ ���� ��ġ�Ͽ� ���� �ð��� �������մϴ�.\n\n'�ٴ� �ð��ϱ�'�� Ŭ�����ּ���.";
		
			GroundInstall_CV_P1.state = (int)ItemTag_P1.Spring;
		}

	}

	public void OnClickRubberBtn( Vector3 position )
	{
		GameObject rubberGo =  Instantiate(rubber.gameObject , GroundInstall_CV_P1.SpawnedItemsHolder);

		rubberGo.transform.position = position;
		rubberGo.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
		rubberGo.transform.localScale = Vector3.one * 0.2f;
	}

	[System.Serializable]
	public class RubberButton
	{
		public Button button;
		public bool isClicked;
		public void OnClickButton()
		{
			isClicked = true;

			button.interactable = false;
			button.GetComponent<Image>().DOFade(0f, 1f);
		}
	}

	[System.Serializable]
	public class Rubber
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
