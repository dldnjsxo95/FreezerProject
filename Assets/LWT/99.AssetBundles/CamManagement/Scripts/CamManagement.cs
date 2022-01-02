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
	public Transform positionSetting;
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

	CamTarget SearchTarget(string targetName)
	{
		for (int i = 0; i < camTargets.Count; i++)
		{
			if (camTargets[i].name == targetName)
				return camTargets[i];
		}

		return null;
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

	[ContextMenu("SetTarget")]
	public void SetTarget()
	{
		camTargets.Clear();

		for (int i = 0; i < positionSetting.childCount; i++)
		{
			string name = positionSetting.GetChild(i).name;
			Transform camPos = positionSetting.GetChild(i).GetChild(0);
			Transform lookPos = positionSetting.GetChild(i).GetChild(1);

			camTargets.Add(new CamTarget(name, camPos, lookPos));
		}
	}

	[System.Serializable]
	public class CamTarget
	{
		public string name;
		public Transform playerArrivePos;
		public Transform playerLookAtPos;

		public CamTarget(string name, Transform camPos, Transform lookPos)
		{
			this.name = name;
			playerArrivePos = camPos;
			playerLookAtPos = lookPos;
		}

	}
}

