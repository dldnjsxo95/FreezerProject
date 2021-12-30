using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Navigation : MonoBehaviour
{
	public float fadeTime = 0.3f;

	[Header("# �̹��� �������� Navigation(Script) ���콺 ��Ŭ�� �� 'SetNavDirection'�� �����ϸ� ����� ������ ���� �ȴ�.")]
	public FadeObject[] arrowFadeObjs;
	public Transform lastTarget;

	Coroutine navRoutine;

	private void OnEnable()
	{
		navRoutine = StartCoroutine(NavRoutine());
	}

	private void OnDisable()
	{
		StopCoroutine(navRoutine);
	}

	[ContextMenu("SetNavDirection")]
	public void SetNavDirection()
	{
		arrowFadeObjs = GetComponentsInChildren<FadeObject>();

		for (int i = 0; i < arrowFadeObjs.Length; i++)
		{
			arrowFadeObjs[i].transform.name = $"Nav_Img_{i + 1}";

			if (i == arrowFadeObjs.Length - 1) arrowFadeObjs[i].transform.right = lastTarget.position - arrowFadeObjs[i].transform.position;
			else arrowFadeObjs[i].transform.right = arrowFadeObjs[i + 1].transform.position - arrowFadeObjs[i].transform.position;
		}
	}

	IEnumerator NavRoutine()
	{
		int idx = 0;

		arrowFadeObjs.ToList().ForEach(x => x.SetAlphaZero());

		while (true)
		{
			arrowFadeObjs[idx].SetFadeIn(fadeTime);

			yield return new WaitForSeconds(fadeTime);

			arrowFadeObjs[idx++].SetFadeOut(fadeTime * (arrowFadeObjs.Length -1));

			if (idx > arrowFadeObjs.Length - 1) idx = 0;

			arrowFadeObjs[idx].SetFadeIn(fadeTime);

			yield return null;
		}
	}
}
