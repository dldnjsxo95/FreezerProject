using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PipeTextBox_P2 : MonoBehaviour
{
	[Header("CamManagement")]
	public CamManagement_P2 camManagement;

	[Header("Button Setting")]
	public Button nextBtn;
	public Button prevBtn;
	public Button lastBtn;

	[Header("Activation Setting")]
	public Objects objects;

	[Header("Other Setting")]
	public FadeObject finishPanel;

	[Header("Contents Setting")]
	public Text eduTxt;
	public Content[] contents;


	[HideInInspector] public MoveObject trMoveObj;

	int txtIdx;
	int TxtIds
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

		trMoveObj = GetComponent<MoveObject>();
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
		TxtIds = 0;

		ActionContents(TxtIds);

		TurnOffLastButton();
	}

	public void OnClickNextBtn()
	{
		TxtIds++;

		ActionContents(TxtIds);
	}

	public void OnClickPrevBtn()
	{
		TxtIds--;

		ActionContents(TxtIds);
	}

	public void OnClickLastBtn()
	{
		finishPanel.gameObject.SetActive(true);
		finishPanel.SetOnObject(0.5f);
	}

	void ActionContents(int txtIdx)
	{
		if (camManagement != null)
			camManagement.MoveTo(contents[TxtIds].targetTag);

		eduTxt.text = contents[TxtIds].text;

		contents[txtIdx].SetObjectsActivation();

		contents[TxtIds].onClick.Invoke();

	}

	public void DetectGas()
	{
		StartCoroutine(DetectGasRoutine());
	}

	IEnumerator DetectGasRoutine()
	{
		objects.detectGas.gameObject.SetActive(true);

		yield return null;

		objects.detectGas.gameObject.SetActive(false);

		yield return new WaitForSeconds(3f);

		eduTxt.text = "";

		objects.detectGas.gameObject.SetActive(true);

		objects.detectGas.GetComponent<DetectGas_P2>().SetOnEnable();

		trMoveObj.MovingOut();
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

	public void SetDisableNextButton()
	{
		nextBtn.interactable = false;
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
		public TargetTag_P2 targetTag;
		[TextArea(5, 10)]
		public string text;
		public Activation activation;
		public UnityEvent onClick;

		[System.NonSerialized]
		public Objects objects;

		public void SetObjectsActivation()
		{
			objects.clicker.SetActive(activation.clicker);
			objects.chooseGas_Panel.SetActive(activation.chooseGas_Panel);
			objects.detectGas.SetActive(activation.detectGas);
			objects.adjustPressure.SetActive(activation.adjustPressure);
		}
	}

	[System.Serializable]
	public class Objects
	{
		public GameObject clicker;
		public GameObject chooseGas_Panel;
		public GameObject detectGas;
		public GameObject adjustPressure;
	}

	[System.Serializable]
	public class Activation
	{
		public bool clicker;
		public bool chooseGas_Panel;
		public bool detectGas;
		public bool adjustPressure;
	}

}
