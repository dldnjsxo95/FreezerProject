using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateCameraController.Cameras.Controllers; 

public class PlayerEvent : MonoBehaviour
{
	public static PlayerEvent Instance;

	public PlayerMove playerMove;
	public CamRotate camRotate;
	public CameraController cameraController;
	public CameraZoom cameraZoom;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.Log($"{gameObject.name} : have 2 PlayerEvent Scripts");
			Destroy(this);
		}
	}
}

