using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UltimateCameraController.Cameras.Controllers;

public class DetectGas_P2 : MonoBehaviour
{
	public CamManagement_P2 camManagement;
	public PipeTextBox_P2 pipeTextBox;

	[Header("Button")]
	public Button nextBtn;
	public Tank aseTank;
	public Tank oxyTank;

	[Header("Camera")]
	public CameraZoom cameraZoom;
	public CameraController cameraController;

	[Header("Spray Setting")]
	public Animator sprayAse_Anim;
	public Animator sprayOxy_Anim;

	[Header("Brush")]
	public Transform brushBody;
	public Animator brushAnim;

	[Header("Rotate Setting")]
	public Transform playerCamera;
	public Slider aseSlider;
	public Slider oxySlider;

	[Header("OutLine Setting")]
	public Outline sprayAse_OutLine;
	public Outline sprayOxy_OutLine;
	public Outline gaugeAse_OutLine;
	public Outline gaugeOxy_OutLine;
	public Outline brush_OutLine;


	Coroutine aseRotRoutine;
	Coroutine oxyRotRoutine;
	bool isAseFinished;
	bool isOxyFinished;

	bool isStared;

	private void Awake()
	{
		sprayAse_Anim.gameObject.SetActive(false);
		sprayOxy_Anim.gameObject.SetActive(false);
		brushBody.gameObject.SetActive(false);
		aseTank.SetOffAllCVs();
		oxyTank.SetOffAllCVs();
		aseSlider.gameObject.SetActive(false);
		oxySlider.gameObject.SetActive(false);
		gaugeAse_OutLine.enabled = false;
		gaugeOxy_OutLine.enabled = false;
	}

	void Start()
	{
		aseTank.AddListener(OnClickAseBtn, OnClickAseSudsBtn, OnClickAseBrushBtn);
		oxyTank.AddListener(OnClickOxyBtn, OnClickOxySudsBtn, OnClickOxyBrushBtn);
	}

	public void SetOnEnable()
	{
		aseTank.circleCV.SetActive(true);
		oxyTank.circleCV.SetActive(true);

		gaugeAse_OutLine.enabled = true;
		gaugeOxy_OutLine.enabled = true;
	}

	private void OnDisable()
	{
		cameraZoom.enabled = false;
		cameraController.enabled = false;
	}

	private void Update()
	{
		if (isAseFinished && isOxyFinished && !nextBtn.interactable)
		{
			nextBtn.interactable = true;
			Invoke("SetOffBrush", 5f);
			pipeTextBox.eduTxt.text = "확인이 완료 되었습니다.";
			//pipeTextBox.trMoveObj.MovingIn();
		}
	}

	// 동그라미 클릭
	public void OnClickAseBtn()
	{
		if (isAseFinished) return;

		gaugeAse_OutLine.enabled = false;
		if (!isOxyFinished) gaugeOxy_OutLine.enabled = true;

		pipeTextBox.trMoveObj.MovingOut();

		brushBody.gameObject.SetActive(false);

		if (oxyRotRoutine != null) StopCoroutine(oxyRotRoutine);

		oxySlider.gameObject.SetActive(false);
		oxySlider.bar.fillAmount = 0;

		pipeTextBox.eduTxt.text = "";

		aseTank.SetOffAllCVs();
		oxyTank.SetOffAllCVs();

		aseTank.circleCV.gameObject.SetActive(false);
		oxyTank.circleCV.gameObject.SetActive(true);

		aseTank.sudsCV.gameObject.SetActive(true);
		aseTank.sudsBtn.interactable = true;

		oxyTank.sudsCV.gameObject.SetActive(false);

		sprayAse_Anim.gameObject.SetActive(true);

		cameraZoom.enabled = false;
		cameraController.enabled = false;

		camManagement.MoveTo(TargetTag_P2.Gas_Ase);
	}

	public void OnClickOxyBtn()
	{
		if (isOxyFinished) return;

		gaugeOxy_OutLine.enabled = false;
		if (!isAseFinished) gaugeAse_OutLine.enabled = true;

		pipeTextBox.trMoveObj.MovingOut();

		brushBody.gameObject.SetActive(false);

		if (aseRotRoutine != null) StopCoroutine(aseRotRoutine);

		aseSlider.gameObject.SetActive(false);
		aseSlider.bar.fillAmount = 0;

		pipeTextBox.eduTxt.text = "";

		aseTank.SetOffAllCVs();
		oxyTank.SetOffAllCVs();

		oxyTank.circleCV.gameObject.SetActive(false);
		aseTank.circleCV.gameObject.SetActive(true);

		oxyTank.sudsCV.gameObject.SetActive(true);
		oxyTank.sudsBtn.interactable = true;

		aseTank.sudsCV.gameObject.SetActive(false);

		sprayOxy_Anim.gameObject.SetActive(true);

		cameraZoom.enabled = false;
		cameraController.enabled = false;

		camManagement.MoveTo(TargetTag_P2.Gas_Oxy);
	}

	// 비눗방울 클릭
	public void OnClickAseSudsBtn()
	{
		sprayAse_Anim.SetTrigger("SprayAse");
		sprayAse_OutLine.enabled = false;
		aseTank.sudsBtn.interactable = false;

		Invoke("SetOnBrushBtn_Ase", 8f);

	}

	public void SetOnBrushBtn_Ase()
	{
		aseTank.sudsCV.SetActive(false);
		aseTank.brushCV.SetActive(true);
		aseTank.brushBtn.gameObject.SetActive(true);
		aseTank.brushBtn.interactable = true;

		brush_OutLine.enabled = true;

		brushBody.gameObject.SetActive(true);
		brushBody.position = aseTank.brushBtn.gameObject.transform.position;
	}

	public void OnClickOxySudsBtn()
	{
		sprayOxy_Anim.SetTrigger("SprayOxy");
		sprayOxy_OutLine.enabled = false;
		oxyTank.sudsBtn.interactable = false;

		Invoke("SetOnBrushBtn_Oxy", 8f);

	}

	public void SetOnBrushBtn_Oxy()
	{
		oxyTank.sudsCV.SetActive(false);
		oxyTank.brushCV.SetActive(true);
		oxyTank.brushBtn.gameObject.SetActive(true);
		aseTank.brushBtn.interactable = true;

		brush_OutLine.enabled = true;

		brushBody.gameObject.SetActive(true);
		brushBody.position = oxyTank.brushBtn.gameObject.transform.position;
	}

	// 브러시 클릭
	public void OnClickAseBrushBtn()
	{
		aseTank.brushBtn.gameObject.SetActive(false);
		brush_OutLine.enabled = false;

		brushAnim.SetTrigger("CleanAse");

		cameraController.targetObject = aseTank.circleBtn.gameObject.transform;
		cameraZoom.enabled = true;
		cameraController.enabled = true;

		aseRotRoutine = StartCoroutine(CheckRoutine(aseSlider, aseTank));
	}

	public void OnClickOxyBrushBtn()
	{
		oxyTank.brushBtn.gameObject.SetActive(false);
		brush_OutLine.enabled = false;

		brushAnim.SetTrigger("CleanOxy");

		cameraController.targetObject = oxyTank.circleBtn.gameObject.transform;
		cameraZoom.enabled = true;
		cameraController.enabled = true;

		oxyRotRoutine = StartCoroutine(CheckRoutine(oxySlider, oxyTank));
	}

	// 회전
	IEnumerator CheckRoutine(Slider slider, Tank tank)
	{
		yield return new WaitForSeconds(5f);

		pipeTextBox.trMoveObj.MovingIn();
		pipeTextBox.eduTxt.text = "마우스 왼쪽 버튼으로 Drag 하거나 키보드 QE를 통해 좌, 우 회전 시키면서 붓으로 잘 닦았는지 살펴보세요.";

		brushBody.gameObject.SetActive(false);

		slider.gameObject.SetActive(true);
		slider.bar.fillAmount = 0;
		slider.value.text = Mathf.FloorToInt(slider.bar.fillAmount * 100).ToString();

		Quaternion preRotaion = playerCamera.rotation;

		while (slider.bar.fillAmount < 0.99f)
		{
			if (playerCamera.rotation != preRotaion)
			{
				slider.bar.fillAmount += 0.3f * Time.deltaTime;
				slider.value.text = Mathf.FloorToInt(slider.bar.fillAmount * 100).ToString();
			}

			if (slider.bar.fillAmount >= 0.99f)
			{
				slider.bar.fillAmount = 1f;
				slider.value.text = Mathf.FloorToInt(slider.bar.fillAmount * 100).ToString();
			}

			preRotaion = playerCamera.rotation;

			yield return null;
		}

		tank.circleBtn.gameObject.SetActive(false);

		if (slider == aseSlider)
		{
			isAseFinished = true;
			pipeTextBox.eduTxt.text = "남은 산소도 진행해주세요.";
			aseSlider.gameObject.SetActive(false);
		}
		else if (slider == oxySlider)
		{
			oxySlider.gameObject.SetActive(false);
			pipeTextBox.eduTxt.text = "남은 아세틸렌도 진행해주세요.";
			isOxyFinished = true;
		}

	}

	void SetOffBrush()
	{
		brushAnim.gameObject.SetActive(false);
	}

	[System.Serializable]
	public class Tank
	{
		public Button circleBtn;
		public Button sudsBtn;
		public Button brushBtn;
		public Button prBtns;

		[HideInInspector] public GameObject circleCV;
		[HideInInspector] public GameObject sudsCV;
		[HideInInspector] public GameObject brushCV;
		[HideInInspector] public GameObject prCV;

		public void AddListener(UnityAction circleAction, UnityAction sudsAction, UnityAction brushAction)
		{
			circleBtn.onClick.AddListener(circleAction);
			sudsBtn.onClick.AddListener(sudsAction);
			brushBtn.onClick.AddListener(brushAction);
		}

		public void ResetSetting()
		{
			sudsBtn.gameObject.SetActive(false);
			brushBtn.gameObject.SetActive(false);
		}

		public void SetOffAllCVs()
		{
			if (circleCV == null) circleCV = circleBtn.transform.parent.gameObject;
			if (sudsCV == null) sudsCV = sudsBtn.transform.parent.gameObject;
			if (brushCV == null) brushCV = brushBtn.transform.parent.gameObject;
			if (prCV == null) prCV = prBtns.transform.parent.gameObject;

			circleCV.SetActive(false);
			sudsCV.SetActive(false);
			brushCV.SetActive(false);
			prCV.SetActive(false);
		}


	}

	[System.Serializable]
	public class Slider
	{
		public GameObject gameObject;
		public Image bar;
		public Text value;
	}
}
