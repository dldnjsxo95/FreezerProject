using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MoveObjectBtn : MonoBehaviour
{
	public MoveObject[] inObjects;
	public MoveObject[] outObjects;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(ObjectsMovingIn);
		GetComponent<Button>().onClick.AddListener(ObjectsMovingOut);
	}

	void ObjectsMovingIn()
	{
		if (inObjects.Length == 0)
			return;

		inObjects.ToList().ForEach(x => x.MovingIn());

	}

	void ObjectsMovingOut()
	{
		if (outObjects.Length == 0)
			return;

		outObjects.ToList().ForEach(x => x.MovingOut());
	}

}
