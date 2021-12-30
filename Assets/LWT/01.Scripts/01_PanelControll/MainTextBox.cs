using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainTextBox : MonoBehaviour
{
	[Header("Button Setting")]
	public Button nextBtn;
	public Button prevBtn;
	public Button lastBtn;

	[Header("Activation Setting")]
	public Objects objects;

	[Header("Other Setting")]
	public Thermometer_Stick thermometer_Stick;
	public FadeObject finishPanel;

	[Header("Contents Setting")]
	public Text eduTxt;
	public Content[] contents;

	int txtIdx;
	public int PreTxtIds { get; set; }
	public int TxtIds
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
		eduTxt.text = contents[TxtIds].text;

		contents[txtIdx].SetObjectsActivation();

		contents[TxtIds].onClick.Invoke();

		if (CamManagement.Instance != null)
			CamManagement.Instance.MoveTo(contents[TxtIds].targetTag);

		PreTxtIds = TxtIds;
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

	// -------------- Thermometer ---------------------

	Coroutine checkTemperRoutine;

	public void StartCheckRoutine(string contents)
	{
		checkTemperRoutine = StartCoroutine(CheckTemperRoutine(contents));
	}

	IEnumerator CheckTemperRoutine(string contents)
	{
		yield return new WaitForSeconds(4f);

		yield return new WaitUntil(() => !objects.thermometer.activeInHierarchy || thermometer_Stick.IsChecked);

		if (thermometer_Stick.IsChecked)
			eduTxt.text = contents;
	}


	[System.Serializable]
	public class Content
	{
		public string name;
		public TargetTag targetTag;
		[TextArea(5, 10)]
		public string text;
		public Activation activation;
		public UnityEvent onClick;

		[System.NonSerialized]
		public Objects objects;

		public void SetObjectsActivation()
		{
			objects.condenser_Gas.SetActive(activation.condenser_Gas);
			objects.condenser_Water.SetActive(activation.condenser_Water);
			objects.thermometer.SetActive(activation.thermometer);
			objects.hole_Water.SetActive(activation.hole_Water);
			objects.evaporator_Water.SetActive(activation.evaporator_Water);
			objects.compressor_Gas.SetActive(activation.compressor_Gas);
			objects.compressorVane_Gas.SetActive(activation.compressorVane_Gas);
			objects.menu_CV.SetActive(activation.menu_CV);
			objects.num2.SetActive(activation.num2);
			objects.rotateController.SetActive(activation.rotateController);
		}
	}

	[System.Serializable]
	public class Objects
	{
		public GameObject condenser_Gas;
		public GameObject condenser_Water;
		public GameObject thermometer;
		public GameObject hole_Water;
		public GameObject evaporator_Water;
		public GameObject compressor_Gas;
		public GameObject compressorVane_Gas;
		public GameObject menu_CV;
		public GameObject num2;
		public GameObject rotateController;
	}

	[System.Serializable]
	public class Activation
	{
		public bool condenser_Gas;
		public bool condenser_Water;
		public bool thermometer;
		public bool hole_Water;
		public bool evaporator_Water;
		public bool compressor_Gas;
		public bool compressorVane_Gas;
		public bool menu_CV;
		public bool num2;
		public bool rotateController;

	}

}
