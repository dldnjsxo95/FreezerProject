using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;


public class FadeImg_P3 : MonoBehaviour
{
	public float fadeTime;

	Image[] images;
	Text[] texts;
	List<float> startAlpha = new List<float>();
	Coroutine coroutine;
	public bool IsActived { get; set; }

	void Awake()
	{
		images = GetComponentsInChildren<Image>(true);
		texts = GetComponentsInChildren<Text>(true);

		images.ToList().ForEach(x => startAlpha.Add(x.color.a));
		texts.ToList().ForEach(x => startAlpha.Add(x.color.a));
	}

	private void OnEnable()
	{
		SetFadeIn(fadeTime);
	}

	private void OnDisable()
	{
		if (coroutine != null)
		{
			images.ToList().ForEach(x => DOTween.Kill(x.gameObject));
			texts.ToList().ForEach(x => DOTween.Kill(x.gameObject));

			StopCoroutine(coroutine);
			coroutine = null;
		}
	}

	public void SetFadeIn(float time)
	{
		int idx = 0;

		images.ToList().ForEach(x => { Color color = x.color; color.a = 0; ; x.color = color; x.DOFade(startAlpha[idx], time); idx++; });
		texts.ToList().ForEach(x => { Color color = x.color; color.a = 0; x.color = color; x.DOFade(startAlpha[idx], time); idx++; });

		IsActived = true;
	}

	public void SetFadeOut(float time)
	{
		images.ToList().ForEach(x => { x.DOFade(0, time); });
		texts.ToList().ForEach(x => { x.DOFade(0, time); });

		IsActived = false;
	}

	public void SetOnObject(float time)
	{
		if (coroutine != null)
			StopCoroutine(coroutine);

		SetFadeIn(time);
	}

	public void SetOffObject(float time)
	{
		if (gameObject.activeInHierarchy)
			coroutine = StartCoroutine(SetOff_Cour(time));
	}

	IEnumerator SetOff_Cour(float time)
	{
		SetFadeOut(time);

		yield return new WaitForSeconds(time);

		gameObject.SetActive(false);

		coroutine = null;
	}


	IEnumerator OnOff_Cour(float time)
	{
		SetFadeIn(time);

		yield return new WaitForSeconds(time + 1f);

		SetOffObject(time);

		coroutine = null;
	}

}


