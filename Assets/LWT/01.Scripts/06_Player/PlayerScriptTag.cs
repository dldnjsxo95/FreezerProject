using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerScriptTag { PlayerMove, CamRotate, CameraZoom, CameraController }

public class PlayerScript : MonoBehaviour
{
	public new PlayerScriptTag tag;
}
