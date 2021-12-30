using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap02_P1 : CubeMap_P1
{
	[Header("Button Setting")]
	public Button openSibangSeo_Btn;
	public Button closeSibangSeo_Btn;

	[Header("Items Setting")]
	public GameObject mainTextBox;
	public GameObject sibangSeoPanel;

	private void Awake()
	{
		openSibangSeo_Btn.onClick.AddListener(OnClickOpenSibangBtn);
		closeSibangSeo_Btn.onClick.AddListener(OnClickCloseSibangBtn);
	}

	private void OnEnable()
	{
		mainTextBox.SetActive(true);
		sibangSeoPanel.SetActive(false);
	}

	public void OnClickOpenSibangBtn()
	{
		Player.DisableAllScripts();

		mainTextBox.SetActive(false);
		sibangSeoPanel.SetActive(true);
	}

	public void OnClickCloseSibangBtn()
	{
		Player.EnableScripts(new PlayerScriptTag[]{ PlayerScriptTag.CamRotate});

		mainTextBox.SetActive(true);
		sibangSeoPanel.SetActive(false);
	}

}
