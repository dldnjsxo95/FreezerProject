using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UIRenderer : MonoBehaviour
{
	[SerializeField]
	private RawImage _uiRenerTexture;

	private Vector2Int _lastScreenSize;
	private Camera _cam;

	void Start()
	{
		_lastScreenSize = new Vector2Int(Screen.width, Screen.height);

		CreateRenderTexture();
	}

	void Update()
	{
		if (_lastScreenSize.x != Screen.width || _lastScreenSize.y != Screen.height)
		{
			_lastScreenSize.x = Screen.width;
			_lastScreenSize.y = Screen.height;

			CreateRenderTexture();
		}
	}

	void CreateRenderTexture()
	{
		_cam = gameObject.GetComponent<Camera>();

		UniversalAdditionalCameraData mainCamData = Camera.main.GetUniversalAdditionalCameraData();
		mainCamData.cameraStack.Clear();

		UniversalAdditionalCameraData camData = _cam.GetUniversalAdditionalCameraData();
		camData.renderType = CameraRenderType.Base;
		camData.antialiasing = AntialiasingMode.None;

		_cam.clearFlags = CameraClearFlags.SolidColor;

		_uiRenerTexture.color = Color.white;

		UpdateRenderTexture();
	}

	void UpdateRenderTexture()
	{
		_cam.targetTexture = new RenderTexture(_lastScreenSize.x, _lastScreenSize.y, 24);

		_uiRenerTexture.texture = _cam.targetTexture;
	}
}
