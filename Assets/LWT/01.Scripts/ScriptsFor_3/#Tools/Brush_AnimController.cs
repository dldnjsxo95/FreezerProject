using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush_AnimController : MonoBehaviour
{
	public Animator animator;

	[Header("Anim Controll")]
	public string play = "Play";
	public string stop = "Stop";
	public float size;

	[Header("Success Setting")]
	public GameObject accumDirty;
	public TextBox_P3 textBox_P3;
	public EnableController accumOilEnabler;

	float currentTime;
	bool isAnimPlayed;

	Vector3 handPos;
	Vector3 handSize;

	private void Awake()
	{
		handPos = transform.localPosition;
		handSize = transform.localScale;
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			int ignoreLayer = 1 << LayerMask.NameToLayer("ignoreLayer");

			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ~ignoreLayer))
			{
				if (hitInfo.collider.CompareTag("AccumDirty"))
				{
					transform.position = hitInfo.point;
					transform.localScale = size * Vector3.one;
					currentTime += Time.deltaTime;

					if (!isAnimPlayed)
					{
						isAnimPlayed = true;
						animator.SetTrigger(play);
					}

					if (currentTime >= 2)
					{
						currentTime = 0;

						SetStop();

						accumDirty.SetActive(false);
						textBox_P3.textBox_Txt.text = "���ϼ̽��ϴ�.\n���������� ��ü�� ȸ���Ǵ��� Ȯ���غ�����.";
						accumOilEnabler.gameObject.SetActive(false);
						textBox_P3.nextBtn.interactable = true;
					}
				}
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			SetStop();
		}
	}

	private void SetStop()
	{
		transform.localPosition = handPos;
		transform.localScale = handSize;

		if (isAnimPlayed)
		{
			animator.SetTrigger(stop);
			isAnimPlayed = false;
		}
	}

}
