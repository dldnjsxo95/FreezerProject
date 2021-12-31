using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class Item_SpringCV_P1 : ItemCV_P1
{
	[Header("Item Setting")]
	public Spring spring;

	[Header("Button Setting")]
	public SpringButton[] springButtons;

	public override void Init()
	{
		spring.gameObject.SetActive(true);
		spring.SetPosPlayerHand();

		springButtons.ToList().ForEach(x =>
		{
			x.button.onClick.AddListener(x.OnClickButton);
			x.button.onClick.AddListener(() => OnClickRubberBtn(x.button.transform.position, x.rotation));
		});

		CubeMap04_P1.Map4_Txt.text = "방진 스프링을 바닥에 설치해 주세요.";
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

		if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, ~ignoreLayer))
		{
			if (hitInfo.collider.CompareTag("AlignerCheckBox"))
			{
				spring.transform.position = hitInfo.point;
				spring.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
				spring.transform.localScale = Vector3.one * 400f;
			}
			else if (hitInfo.collider.CompareTag("Button"))
			{
				spring.transform.position = hitInfo.transform.position;
			}
		}

		int clickedCount = 0;

		for (int i = 0; i < springButtons.Length; i++)
		{
			if (springButtons[i].isClicked) clickedCount++;
		}

		if (clickedCount == springButtons.Length)
		{
			CubeMap04_P1.GroundInstall_Btn.interactable = true;
			CubeMap04_P1.Map4_Txt.text = "방진 스프링 설치가 완료되었다면 천장과 터보냉동기를 연결하는 천장 드래드 라인을 설치해보겠습니다.\n\n'바닥 시공하기 버튼'을 선택해주세요.";

			GroundInstall_CV_P1.state = (int)ItemTag_P1.Tape;
		}

	}

	public void OnClickRubberBtn(Vector3 position, Vector3 rotation)
	{
		GameObject rubberGo = Instantiate(spring.pref, GroundInstall_CV_P1.SpawnedItemsHolder);

		rubberGo.transform.position = position;
		rubberGo.transform.localRotation = Quaternion.Euler(rotation);
		rubberGo.transform.localScale = Vector3.one * 0.2f;
	}

	[System.Serializable]
	public class SpringButton
	{
		public Vector3 rotation;
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
	public class Spring
	{
		public GameObject gameObject;
		public GameObject pref;
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
