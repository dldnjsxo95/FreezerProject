using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateCameraController.Cameras.Controllers;

public class CamControllerSetTarget : MonoBehaviour
{
	public CameraController cameraController;
	public List<GameObject> gameObjects;

	public void SetTarget(string name)
	{
		cameraController.targetObject = gameObjects.Find(x => x.name == name).transform;
	} 


}
