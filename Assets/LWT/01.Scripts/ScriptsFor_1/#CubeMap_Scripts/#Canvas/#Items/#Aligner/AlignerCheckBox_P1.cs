using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignerCheckBox_P1 : MonoBehaviour
{
	public bool IsChecked { get; set; } = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item")) IsChecked = true;
	}
}
