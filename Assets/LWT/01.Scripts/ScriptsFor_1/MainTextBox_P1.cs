using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainTextBox_P1 : MonoBehaviour
{
	[Header("CamManagement")]
	public CamManagement_P1 camManagement;

	[Header("Button Setting")]
	public Button nextBtn;
	public Button prevBtn;
	public Button lastBtn;

	[Header("Other Settings")]
	public FadeObject finishPanel;

	[Header("Activation Setting")]
	public Objects objects;

	[Header("Contents Setting")]
	public Text eduTxt;
	public Content[] contents;

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
		ActivationSetting();
	}

	private void Start()
	{
		nextBtn.onClick.AddListener(OnClickNextBtn);
		prevBtn.onClick.AddListener(OnClickPrevBtn);
		lastBtn.onClick.AddListener(OnClickLastBtn);

		contents[contents.Length - 2].onClick.AddListener(TurnOffLastButton);
		contents[contents.Length - 1].onClick.AddListener(TurnOnLastButton);
	}

	private void OnEnable()
	{
		TxtIdx = 0;

		ActionContents(TxtIdx);

		TurnOffLastButton();
	}

	public void OnClickNextBtn()
	{
		TxtIdx++;

		ActionContents(TxtIdx);
	}

	public void OnClickPrevBtn()
	{
		TxtIdx--;

		ActionContents(TxtIdx);
	}

	public void OnClickLastBtn()
	{
		finishPanel.gameObject.SetActive(true);
		finishPanel.SetOnObject(0.5f);
	}

	void ActionContents(int txtIdx)
	{
		if (camManagement != null)
			camManagement.MoveTo(contents[TxtIdx].targetTag);

		eduTxt.text = contents[TxtIdx].text;

		contents[txtIdx].SetObjectsActivation();

		contents[TxtIdx].onClick.Invoke();

	}

	// -------------- Button Setting---------------

	public void TurnOnLastButton()
	{
		nextBtn.gameObject.SetActive(false);
		lastBtn.gameObject.SetActive(true);
	}

	public void TurnOffLastButton()
	{
		nextBtn.gameObject.SetActive(true);
		lastBtn.gameObject.SetActive(false);
	}

	// -------------- Activation Setting ---------------

	void ActivationSetting()
	{
		foreach (Content content in contents)
		{
			content.objects = objects;
		}
	}

	[System.Serializable]
	public class Content
	{
		public string name;
		public TargetTag_P1 targetTag;
		[TextArea(5, 10)]
		public string text;
		public Activation activation;
		public UnityEvent onClick;

		[System.NonSerialized]
		public Objects objects;

		public void SetObjectsActivation()
		{
			objects.groundLine.SetActive(activation.groundLine);
			objects.rotateBtn.SetActive(activation.rotateBtn);
			objects.downUpBtn.SetActive(activation.downUpBtn);
			objects.specification_1.SetActive(activation.specification_1);
			objects.specification_2.SetActive(activation.specification_2);
			objects.specification_3.SetActive(activation.specification_3);
			objects.specification_4.SetActive(activation.specification_4);
			objects.specification_5.SetActive(activation.specification_5);
			objects.ruler.SetActive(activation.ruler);
			objects.rubberCV.SetActive(activation.rubberCV);
			objects.springCV.SetActive(activation.springCV);
			objects.wayCheck_1.SetActive(activation.wayCheck_1);
			objects.wayCheck_2.SetActive(activation.wayCheck_2);
			objects.ImgSlider.SetActive(activation.ImgSlider);
			objects.turbo_Refrigerator.SetActive(activation.turbo_Refrigerator);
			objects.CheckPaint_CV.SetActive(activation.CheckPaint_CV);
		}
	}

	[System.Serializable]
	public class Objects
	{
		public GameObject groundLine;
		public GameObject rotateBtn;
		public GameObject downUpBtn;
		public GameObject specification_1;
		public GameObject specification_2;
		public GameObject specification_3;
		public GameObject specification_4;
		public GameObject specification_5;
		public GameObject ruler;
		public GameObject rubberCV;
		public GameObject springCV;
		public GameObject wayCheck_1;
		public GameObject wayCheck_2;
		public GameObject ImgSlider;
		public GameObject turbo_Refrigerator;
		public GameObject CheckPaint_CV;
	}

	[System.Serializable]
	public class Activation
	{
		public bool groundLine;
		public bool rotateBtn;
		public bool downUpBtn;
		public bool specification_1;
		public bool specification_2;
		public bool specification_3;
		public bool specification_4;
		public bool specification_5;
		public bool ruler;
		public bool rubberCV;
		public bool springCV;
		public bool wayCheck_1;
		public bool wayCheck_2;
		public bool ImgSlider;
		public bool turbo_Refrigerator;
		public bool CheckPaint_CV;
	}
}
