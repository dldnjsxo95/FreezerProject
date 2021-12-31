using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TargetTag_P1
{
	NoneUse,
	Installation_Preview,
	GroundView,
}

public class CamManagement_P1 : MonoBehaviour
{
	public static CamManagement_P1 Instance;

	[Header("Move Setting"), Tooltip("Choose Movement Style")]
	public Ease moveStyle;
	public Transform player;
	public Transform playerCam;
	[Tooltip("Time it takes arrive to target")]
	public float duration = 2f;
	public bool IsFinishCamMove { get; set; }

	[Header("Target Setting")]
	public List<CamTarget> camTargets = new List<CamTarget>();

	Coroutine moveRoutine;

	public void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else Destroy(gameObject);
	}

	public void MoveTo(TargetTag_P1 targetTag)
	{
		CamTarget curTarget = SearchTarget(targetTag);

		if (curTarget == null)
		{
			Debug.Log($"There is No Target.tag == {targetTag}");
			return;
		}

		if (moveRoutine != null)
		{
			StopCoroutine(moveRoutine);
			moveRoutine = null;
		}

		moveRoutine = StartCoroutine(MoveRoutine(curTarget));

	}

	CamTarget SearchTarget(TargetTag_P1 targetTag)
	{
		for (int i = 0; i < camTargets.Count; i++)
		{
			if (camTargets[i].targetTag == targetTag)
				return camTargets[i];
		}

		return null;
	}

	IEnumerator MoveRoutine(CamTarget curTarget)
	{
		IsFinishCamMove = false;

		player.DOMove(curTarget.playerArrivePos.position, duration).SetEase(moveStyle);

		float angleOffSet = Vector3.Angle(playerCam.forward, curTarget.playerLookAtPos.position - playerCam.position);

		while (angleOffSet > 0.4f)
		{
			angleOffSet = Vector3.Angle(playerCam.forward, curTarget.playerLookAtPos.position - playerCam.position);

			playerCam.forward = Vector3.Lerp(playerCam.forward, 5*(curTarget.playerLookAtPos.position - playerCam.position).normalized, Time.deltaTime / 2);

			yield return null;
		}

		IsFinishCamMove = true;
	}


	[System.Serializable]
	public class CamTarget
	{
		public string name;
		public TargetTag_P1 targetTag;
		public Transform playerArrivePos;
		public Transform playerLookAtPos;
	}
}
