using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class TextBox_P3 : MonoBehaviour
{
	[Header("Button Setting")]
	public Button prevBtn;
	public Button nextBtn;

	[Header("Item Setting")]
	public Transform itemsHolder;
	public GameObject[] items;

	[Header("Contents Setting")]
	public Text textBox_Txt;
	public Content[] contents;

	[HideInInspector] public MoveObject moveObject;
	[HideInInspector] public FadeObject fadeObject;

	int txtIdx = 0;
	int TxtIdx
	{
		get { return txtIdx; }
		set
		{
			txtIdx = value;

			prevBtn.interactable = true;
			nextBtn.interactable = true;

			if (txtIdx == 0)
				prevBtn.interactable = false;

			if (txtIdx == contents.Length - 1)
				nextBtn.interactable = false;
		}
	}

	private void Awake()
	{
		nextBtn.onClick.AddListener(OnClickNextBtn);
		prevBtn.onClick.AddListener(OnClickPrevBtn);

		moveObject = GetComponent<MoveObject>();
		fadeObject = GetComponent<FadeObject>();

		SetItems();

		TxtIdx = 0;
	}

	private void OnEnable()
	{
		ContentsEvent(TxtIdx);
	}

	[ContextMenu("SetItems")]
	public void SetItems()
	{
		items = new GameObject[itemsHolder.childCount];

		for (int i = 0; i < itemsHolder.childCount; i++)
		{
			items[i] = itemsHolder.GetChild(i).gameObject;
		}
	}


	public void OnClickNextBtn() => ContentsEvent(TxtIdx++);
	public void OnClickPrevBtn() => ContentsEvent(TxtIdx--);
	private void ContentsEvent(int txtIdx)
	{
		textBox_Txt.text = contents[TxtIdx].text;

		ActiveItem(contents[TxtIdx].activeItemGameObjects);

		contents[TxtIdx].onClick.Invoke();
	}
	private void ActiveItem(GameObject[] itemGOs)
	{
		foreach (GameObject item in items)
		{
			bool isContained = false;

			for (int i = 0; i < itemGOs.Length; i++)
			{
				if (item == itemGOs[i])
				{
					isContained = true;
					break;
				}
			}

			if (isContained) item.SetActive(true);
			else item.SetActive(false);
		}
	}

	[System.Serializable]
	public class Content
	{
		public string name;

		[TextArea(5, 10)]
		public string text;

		public GameObject[] activeItemGameObjects;

		public UnityEvent onClick;
	}
}


