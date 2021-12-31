using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Navigation3D : MonoBehaviour
{
	public List<Transform> navObjects = new List<Transform>();

	WaitForSeconds waitForSeconds = new WaitForSeconds(0.15f);
	private void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			navObjects.Add(transform.GetChild(i).GetComponent<Transform>());
		}

		StartCoroutine(Navigating());
	}

	IEnumerator Navigating()
	{
		navObjects.ToList().ForEach(x => x.gameObject.SetActive(false));

		int idx = 0;

		while (true)
		{

			navObjects[idx - 1 >= 0 ? idx - 1 : navObjects.Count - 1].gameObject.SetActive(false);
			navObjects[idx].gameObject.SetActive(true);
			idx = idx == navObjects.Count - 1 ? idx = 0 : idx + 1;
			yield return waitForSeconds;
		}
	}

}
