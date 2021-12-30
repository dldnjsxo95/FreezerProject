using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeMapManager_P1 : MonoBehaviour
{
	static CubeMap_P1[] cubeMaps;
	
	private void Awake()
	{
		cubeMaps = FindObjectsOfType<CubeMap_P1>(true);

		GameManagerP1.StartEvent += () => SetActive(CubeMapP1_Tag.Map1);
	}

    public static void SetActive(CubeMapP1_Tag cubeMap_Tag)
	{
		if (cubeMap_Tag == CubeMapP1_Tag.None) { SetOffAll(); return; } 

		for (int i = 0; i < cubeMaps.Length; i++)
		{
			if (cubeMaps[i].cubeMap_Tag == cubeMap_Tag) cubeMaps[i].gameObject.SetActive(true);
			else cubeMaps[i].gameObject.SetActive(false);
		}
	}

	public static void SetOffAll()
	{
		cubeMaps.ToList().ForEach(x => x.gameObject.SetActive(false));
	}

}
