using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class TextBox_P1 : MonoBehaviour
{
	[Header("Button Setting")]
	public Button prevBtn;
	public Button nextBtn;

	[Header("Item Setting")]
	public GameObject[] items;

	[Header("Contents Setting")]
	public Text textBox_Txt;
	public Content[] contents;

	[HideInInspector] public MoveObject moveObject;
	[HideInInspector] public FadeObject fadeObject;

	int txtIdx;
	int TxtIdx
	{
		get { return txtIdx; }
		set
		{
			txtIdx = value;

			if (txtIdx == 0)
				prevBtn.interactable = false;
			else if (txtIdx == contents.Length - 1)
				nextBtn.interactable = false;
			else
			{
				prevBtn.interactable = true;
				nextBtn.interactable = true;
			}
		}
	}

	private void Awake()
	{
		nextBtn.onClick.AddListener(OnClickNextBtn);
		prevBtn.onClick.AddListener(OnClickPrevBtn);

		moveObject = GetComponent<MoveObject>();
		fadeObject = GetComponent<FadeObject>();
	}

	private void OnEnable()
	{
		ContentsEvent(TxtIdx);
	}

	public void OnClickNextBtn() => ContentsEvent(TxtIdx++);
	public void OnClickPrevBtn() => ContentsEvent(TxtIdx--);
	private void ContentsEvent(int txtIdx)
	{
		textBox_Txt.text = contents[TxtIdx].text;

		ActiveItem(contents[TxtIdx].activeItems);

		contents[TxtIdx].onClick.Invoke();
	}
	private void ActiveItem(string[] itemNames)
	{
		itemNames.ToList().ForEach(x => x = x.Replace(" ", string.Empty));

		foreach (GameObject item in items)
		{
			bool isContained = false;

			for (int i = 0; i < itemNames.Length; i++)
			{
				if (item.name.Contains(itemNames[i]))
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

		public string[] activeItems;

		public UnityEvent onClick;
	}
}


