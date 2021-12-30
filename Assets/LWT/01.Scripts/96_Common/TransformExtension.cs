using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class TransformExtension
{
	public static T[] GetComponentsInChildren<T>( this Transform transform , string name)
	{
		Transform[] array = transform.GetComponentsInChildren<Transform>(true);

		List<T> newObjects = new List<T>();
		array.ToList().ForEach(x => { if (x.name.Contains(name)) newObjects.Add(x.GetComponent<T>());});

		return newObjects.ToArray();
	}

	public static T[] GetComponentsInChildren<T>(this Transform transform, int ignorelayer)
	{
		Transform[] array = transform.GetComponentsInChildren<Transform>(true);

		List<T> newObjects = new List<T>();
		array.ToList().ForEach(x => {if (x.gameObject.GetComponent<T>() != null && x.gameObject.layer != ignorelayer) newObjects.Add(x.GetComponent<T>()); });

		return newObjects.ToArray();
	}


}
