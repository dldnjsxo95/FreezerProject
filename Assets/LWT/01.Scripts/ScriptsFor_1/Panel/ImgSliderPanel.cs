using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ImgSliderPanel : MonoBehaviour
{
	public FadeObject[] sliders;

	Coroutine sliderRoutine;

	[Header("ButtonSetting")]
	public Button[] buttons;

	bool IsChecked { get; set; } = false;

	private void OnEnable()
	{
		sliderRoutine = StartCoroutine(SliderRoutine());
	}

	private void OnDisable()
	{
		StopCoroutine(sliderRoutine);
	}

	private IEnumerator SliderRoutine()
	{
		int idx = 0;

		sliders.ToList().ForEach(x => x.gameObject.SetActive(false));

		while(idx < sliders.Length)
		{
			sliders[idx].gameObject.SetActive(true);

			if (idx > 0) sliders[idx - 1].SetOffObject(0.5f);

			idx++;

			yield return new WaitForSeconds(2f);
		}

		IsChecked = true;
		buttons.ToList().ForEach(x => x.interactable = true);
	}

	public void SetDisableBtns()
	{
		if(!IsChecked) buttons.ToList().ForEach(x => x.interactable = false);
	}

}
