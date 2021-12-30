using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCheck_P2 : MonoBehaviour
{
	public GameObject check_go;
	public EquipmentImg_P2[] eImgs;

	Coroutine invokeRoutine;

	public void Invoke()
	{
		if (invokeRoutine != null) StopCoroutine(invokeRoutine);

		invokeRoutine = StartCoroutine(CheckRoutine());
	}

	IEnumerator CheckRoutine()
	{
		int count = 0;

		while ( count < eImgs.Length)
		{
			count = 0;

			for (int i =0; i< eImgs.Length; i++)
			{
				if (eImgs[i].IsEquiped)
					count++;
			}

			yield return null;
		}

		check_go.SetActive(true);
	}
}
