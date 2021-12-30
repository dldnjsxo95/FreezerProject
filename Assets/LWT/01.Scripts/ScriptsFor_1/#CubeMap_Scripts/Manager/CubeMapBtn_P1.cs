using UnityEngine;
using UnityEngine.UI;

public class CubeMapBtn_P1 : MonoBehaviour
{
	public CubeMapP1_Tag cubeMap_Tag;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(() => CubeMapManager_P1.SetActive(cubeMap_Tag));
	}
}
