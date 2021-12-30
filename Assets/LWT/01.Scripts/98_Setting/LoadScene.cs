using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	public static string choosenScene;

	public void LoadLoginScene(string sceneName)
	{
		choosenScene = sceneName;
		SceneManager.LoadScene("LoginScene");
	}

	public void LoadLoadingScene()
	{
		LoadingSceneManager.LoadScene(choosenScene);
	}

	public void StartSceneDirectly(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void LoadStartScene()
	{
		SceneManager.LoadScene("StartScene");
	}

}
