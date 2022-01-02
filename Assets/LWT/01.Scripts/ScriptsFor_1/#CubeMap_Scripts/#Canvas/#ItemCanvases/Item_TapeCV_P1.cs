using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item_TapeCV_P1 : ItemCV_P1
{
	private enum State { Spawn_CeilLine, Spawn_CoolingWaterLine, Spawn_RefrigerantLine, Taping_RefrigerantLine }
	private State state;

	[Header("Item Setting")]
	public Tape tape;
	[Header("Pipes Setting")]
	public Pipes pipes;
	[Header("Material Setting")]
	public PipeMat pipeMat;

	public override void Init()
	{
		tape.gameObject.SetActive(false);
		tape.SetPosPlayerHand();

		pipes.SetOffAllPipes();
		pipes.SetOnLine(pipes.ceilLine);

		CubeMap04_P1.Map4_Txt.text = "õ��� �ͺ��õ��⸦ �����ϴ� õ�� �巡�� ������ ��ġ�غ��ڽ��ϴ�.\n\n���콺 Ŭ������ õ�� �巹�� ������ ��ġ���ּ���.";
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Pipe_P1 hitedPipe = null;

		int ignoreLayer = 1 << LayerMask.NameToLayer("IgnoreLayer");

		Vector3 tapePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 4));
		tape.transform.position = tapePos;

		if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, ~ignoreLayer))
		{
			if (hitInfo.collider.CompareTag("Pipe"))
			{
				hitedPipe = hitInfo.collider.GetComponent<Pipe_P1>();
			}
		}

		if (Input.GetMouseButtonDown(0) && hitedPipe != null)
		{
			if (state != State.Taping_RefrigerantLine)
			{
				hitedPipe.MeshRenderer.material = pipeMat.chiler_Mat;
				hitedPipe.IsClicked = true;

				switch (state)
				{
					case State.Spawn_CeilLine:
						if (!IsAllClicked(pipes.ceilLine)) return;
						CubeMap04_P1.Map4_Txt.text = "PVC ���� �������� �����Ǿ��ִ� �ð��� �巹�� ������ ��ġ�غ��ڽ��ϴ�.\n\n���콺 Ŭ������ �ð��� �巹�� ������ ��ġ���ּ���.";
						pipes.SetOnLine(pipes.coolingWaterLine);
						state = State.Spawn_CoolingWaterLine;
						break;
					case State.Spawn_CoolingWaterLine:
						if (!IsAllClicked(pipes.coolingWaterLine)) return;
						CubeMap04_P1.Map4_Txt.text = "�̹����� �ø� �巹�� ������ ��ġ�غ��ڽ��ϴ�.\n\n���콺 Ŭ������ �ø� �巹�� ������ ��ġ���ּ���.";
						pipes.SetOnLine(pipes.refrigerantLine);
						state = State.Spawn_RefrigerantLine;
						break;
					case State.Spawn_RefrigerantLine:
						if (!IsAllClicked(pipes.refrigerantLine)) return;
						CubeMap04_P1.Map4_Txt.text = "�µ� �ս��� �����ϱ� ���� �ø� �巹����ο� ���� �������� ���� �ֽñ� �ٶ��ϴ�.\n\n���콺 Ŭ������ �������ּ���.";
						tape.gameObject.SetActive(true);
						pipes.tapeLine.ToList().ForEach(x => x.SetReadyToTapying());
						state = State.Taping_RefrigerantLine;
						break;
				}
			}
			else if (state == State.Taping_RefrigerantLine && hitedPipe.state == Pipe_P1.State.ReadyToTapying)
			{
				hitedPipe.MeshRenderer.material = pipeMat.tapying_Mat;
				hitedPipe.IsClicked = true;
				hitedPipe.state = Pipe_P1.State.Taped;

				if (!IsAllClicked(pipes.tapeLine)) return;

				CubeMap04_P1.RefrigerIn_Btn.interactable = true;
				CubeMap04_P1.Map4_Txt.text = "�����ϼ̽��ϴ�.\n\n�׷� '�õ��� �����ϱ�' ��ư�� ���� �ͺ� �õ��� ������ �������ּ���.";

				this.gameObject.SetActive(false);
			}
		}
	}

	public bool IsAllClicked(Pipe_P1[] pipeLine)
	{
		int clickedCount = 0;

		for (int i = 0; i < pipeLine.Length; i++)
		{
			if (pipeLine[i].IsClicked) clickedCount++;
		}

		return clickedCount == pipeLine.Length ? true : false;
	}

	[System.Serializable]
	public class PipeMat
	{
		public Material chiler_Mat;
		public Material tapying_Mat;
	}


	[System.Serializable]
	public class Pipes
	{
		public Pipe_P1[] ceilLine;
		public Pipe_P1[] coolingWaterLine;
		public Pipe_P1[] refrigerantLine;
		public Pipe_P1[] tapeLine;

		public void SetOffAllPipes()
		{
			ceilLine.ToList().ForEach(x => x.gameObject.SetActive(false));
			coolingWaterLine.ToList().ForEach(x => x.gameObject.SetActive(false));
			refrigerantLine.ToList().ForEach(x => x.gameObject.SetActive(false));
		}

		public void SetOnLine(Pipe_P1[] pipesLine)
		{
			pipesLine.ToList().ForEach(x => x.gameObject.SetActive(true));
		}
	}

	[System.Serializable]
	public class Tape
	{
		public GameObject gameObject;
		public Vector3 startPos;
		public Vector3 rotation;
		public Vector3 size;

		public Transform transform
		{
			get { return gameObject.transform; }
		}

		public void SetPosPlayerHand()
		{
			gameObject.transform.localScale = startPos;
			gameObject.transform.localRotation = Quaternion.Euler(rotation);
			gameObject.transform.localScale = size;
		}
	}
}
