using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathL : MonoBehaviour
{
	public static bool IsInBoundary(float value, float min, float max)
	{
		return min < value && value <= max;
	}

}
