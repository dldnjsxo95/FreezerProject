using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AdjustPressure_P2 : MonoBehaviour
{
	[Header("Button Setting")]
	public Button asePrBtn;
	public Button oxyPrBtn;
	public Transform aseBtn_tr;
	public Transform oxyBtn_tr;

	[Header("OutLine Setting")]
	public Outline aseOutLine;
	public Outline oxyOutLine;

	[Header("Gauge")]
	public Transform aseGauge;
	public Transform oxyGauge;

	[Header("TargetRot")]
	public Vector3 aseRot;
	public Vector3 oxyRot;

	[Header("Next Btn")]
	public Button nextBtn;

	bool isClickedAse;
	bool isClickedOxy;

	private void Awake()
	{
		aseOutLine.enabled = false;
		oxyOutLine.enabled = false;
	}

	private void Start()
	{
		asePrBtn.onClick.AddListener(OnClickAseBtn);
		oxyPrBtn.onClick.AddListener(OnClickOxyBtn);
	}

	private void OnEnable()
	{
		aseBtn_tr.gameObject.SetActive(true);
		oxyBtn_tr.gameObject.SetActive(true);

		aseOutLine.enabled = true;
		oxyOutLine.enabled = true;
	}

	private void OnDisable()
	{
		aseBtn_tr.gameObject.SetActive(false);
		oxyBtn_tr.gameObject.SetActive(false);

		aseOutLine.enabled = false;
		oxyOutLine.enabled = false;
	}

	private void Update()
	{
		if (isClickedAse && isClickedOxy) nextBtn.interactable = true;
	}

	public void OnClickAseBtn()
	{
		aseBtn_tr.gameObject.SetActive(false);
		aseOutLine.enabled = false;

		aseGauge.DOLocalRotateQuaternion(Quaternion.Euler(aseRot), 2f);

		isClickedAse = true;
	}

	public void OnClickOxyBtn()
	{
		oxyBtn_tr.gameObject.SetActive(false);
		oxyOutLine.enabled = false;

		oxyGauge.DOLocalRotateQuaternion(Quaternion.Euler(oxyRot), 2f);

		isClickedOxy = true;
	}

	public void SetNextBtnDisable()
	{
		nextBtn.interactable = false;
	}

}
