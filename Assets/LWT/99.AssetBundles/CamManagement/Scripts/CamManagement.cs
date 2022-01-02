using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TargetTag
{
	NoneUse,
	LookForward,
	LookAtMoveToTurboBtn,
	MoveToTurbo,
	MoveToImpeller,
	MoveToImpeller_2,
	MoveToDiffuser_1,
	MoveToVain_1,
	MoveToLiquidComp_1,
	MoveToCompArrows_1,
	MoveToScreen_1,
	MoveToAccumulators_1,
	MoveToAccumulators_2,
}


public class CamManagement : MonoBehaviour
{
	public static CamManagement Instance;

	[Header("Move Setting"), Tooltip("Choose Movement Style")]
	public Ease moveStyle;
	public Transform player;
	public Transform playerCam;
	[Tooltip("Time it takes arrive to target")]
	public float duration = 2f;

	[Header("Target Setting")]
	public List<CamTarget> camTargets = new List<CamTarget>();

	public bool IsFinishCamMove { get; set; } = true;
	public bool IsRotate { get; set; } = false;

	Coroutine moveRoutine;
	Coroutine moveRoundRoutine;

	public void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else Destroy(gameObject);
	}

	CamTarget SearchTarget(TargetTag targetTag)
	{
		for (int i = 0; i < camTargets.Count; i++)
		{
			if (camTargets[i].targetTag == targetTag)
				return camTargets[i];
		}

		return null;
	}

	CamTarget SearchTarget(string targetName)
	{
		for (int i = 0; i < camTargets.Count; i++)
		{
			if (camTargets[i].name == targetName)
				return camTargets[i];
		}

		return null;
	}

	public void MoveTo(TargetTag targetTag)
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

	public void MoveTo(string targetName)
	{
		CamTarget curTarget = SearchTarget(targetName);

		if (curTarget == null)
		{
			Debug.Log($"There is No Target.tag == {targetName}");
			return;
		}

		if (moveRoutine != null)
		{
			StopCoroutine(moveRoutine);
			moveRoutine = null;
		}

		moveRoutine = StartCoroutine(MoveRoutine(curTarget));
	}

	IEnumerator MoveRoutine(CamTarget curTarget)
	{

		IsFinishCamMove = false;

		player.DOMove(curTarget.playerArrivePos.position, duration).SetEase(moveStyle);

		float angleOffSet = Vector3.Angle(playerCam.forward, curTarget.playerLookAtPos.position - playerCam.position);

		float currentTime = 0;

		while (angleOffSet > 0.1f || currentTime < 1.5f)
		{
			angleOffSet = Vector3.Angle(playerCam.forward, curTarget.playerLookAtPos.position - playerCam.position);

			playerCam.forward = Vector3.Lerp(playerCam.forward, 5 * (curTarget.playerLookAtPos.position - playerCam.position).normalized, Time.deltaTime / 2);

			currentTime += Time.deltaTime;

			yield return null;
		}

		IsFinishCamMove = true;
	}

	//public void MoveRound(string name)
	//{
	//	if (moveRoundRoutine != null)
	//	{
	//		StopCoroutine(moveRoundRoutine);
	//		moveRoundRoutine = null;
	//	}

	//	switch (name)
	//	{
	//		case "MoveToEvaporate":
	//			if(mainTextBox.TxtIds - mainTextBox.PreTxtIds == 1)
	//			{
	//				moveRoundRoutine = StartCoroutine(MoveRoundRoutine(TargetTag.Turbo_PreEvaporator, TargetTag.Turbo_Evaporator));
	//			}
	//			else if(mainTextBox.TxtIds - mainTextBox.PreTxtIds == -1)
	//			{
	//				MoveTo(TargetTag.Turbo_Evaporator);
	//			}
	//			break;

	//		case "MoveToHoleWaterEnter":
	//			print(mainTextBox.TxtIds - mainTextBox.PreTxtIds);
	//			if (mainTextBox.TxtIds - mainTextBox.PreTxtIds == 1)
	//			{
	//				MoveTo(TargetTag.Turbo_HoleWaterEnter);
	//			}
	//			else if (mainTextBox.TxtIds - mainTextBox.PreTxtIds == -1)
	//			{
	//				print("Active");
	//				moveRoundRoutine = StartCoroutine(MoveRoundRoutine(TargetTag.Turbo_PreEvaporator, TargetTag.Turbo_HoleWaterEnter));
	//			}
	//			break;
	//	}
	//}

	IEnumerator MoveRoundRoutine(TargetTag targetTag1, TargetTag targetTag2)
	{
		MoveTo(targetTag1);

		yield return new WaitForSeconds(1.4f);

		MoveTo(targetTag2);
	}


	[System.Serializable]
	public class CamTarget
	{
		public string name;
		public TargetTag targetTag;
		public Transform playerArrivePos;
		public Transform playerLookAtPos;
	}
}

