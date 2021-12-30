using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
	public static string nextScene;

	[Header("BG Setting")]
	[SerializeField] Image bg_Img; 
	[SerializeField] Sprite ref_BG; 
	[SerializeField] Sprite Gas_BG; 

	[Header("Logo Setting")]
	[SerializeField] Image titleLogo_Img; 
	[SerializeField] Sprite process1_Logo; 
	[SerializeField] Sprite process2_Logo; 
	[SerializeField] Sprite process3_Logo; 

	[Header("Loading Setting")]
	[SerializeField] Image progressBar;
	[SerializeField] Image picon;
	[SerializeField] FadeObject continueFO;

	Button continueBtn;
	AsyncOperation op;
	private void Awake()
	{
		continueBtn = continueFO.GetComponent<Button>();
	}

	private void Start()
	{
		continueFO.gameObject.SetActive(false);

		continueBtn.onClick.AddListener(OnClickContinueBtn);

		StartCoroutine(LoadScene());
	}

	public static void LoadScene(string sceneName)
	{
		nextScene = sceneName;

		SceneManager.LoadScene("LoadingScene");
	}

	IEnumerator LoadScene()
	{
		switch (nextScene)
		{
			case "Process1":
				bg_Img.sprite = ref_BG;
				titleLogo_Img.sprite = process1_Logo;
				break;
			case "Process2":
				bg_Img.sprite = Gas_BG;
				titleLogo_Img.sprite = process2_Logo;
				break;
			case "Process3":
				bg_Img.sprite = ref_BG;
				titleLogo_Img.sprite = process3_Logo;
				break;
		}

		yield return null;

		progressBar.fillAmount = 0;

		op = SceneManager.LoadSceneAsync(nextScene);

		op.allowSceneActivation = false;

		float[] randNum = { Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f) };

		while (progressBar.fillAmount < 0.99f)
		{
			picon.rectTransform.localPosition = new Vector3((progressBar.rectTransform.sizeDelta.x * progressBar.fillAmount) - progressBar.rectTransform.sizeDelta.x / 2, 0, 0);

			if (progressBar.fillAmount <= 0.3f)
			{
				progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.5f, randNum[0] * Time.deltaTime);
			}
			else if (MathL.IsInBoundary(progressBar.fillAmount, 0.3f, 0.7f))
			{
				progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.8f, randNum[1] * Time.deltaTime);
			}
			else if (MathL.IsInBoundary(progressBar.fillAmount, 0.7f, 1f))
			{
				progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, randNum[2] * Time.deltaTime);
			}

			yield return null;
		}

		while (!op.isDone)
		{
			if (op.progress >= 0.9f)
			{
				progressBar.fillAmount = 1f;
				continueFO.gameObject.SetActive(true);
				continueFO.SetOnObject(0.5f);
				yield break;
			}

			yield return null;
		}
	}

	public void OnClickContinueBtn()
	{
		op.allowSceneActivation = true;
	}


}

