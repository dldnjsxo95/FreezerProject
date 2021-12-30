using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	static PlayerScript[] playerScripts;

	private void Awake()
	{
		playerScripts = GetComponentsInChildren<PlayerScript>();
	}

	public static void EnableScripts(PlayerScriptTag[] playerScriptTags)
	{
		foreach (PlayerScript playerScript in playerScripts)
		{
			bool isTagEqual = false;

			for (int i = 0; i < playerScriptTags.Length; i++)
			{
				if (playerScript.tag == playerScriptTags[i])
					isTagEqual = true;
			}

			if (isTagEqual) playerScript.enabled = true;
			else playerScript.enabled = false;
		}
	}

	public static void DisableAllScripts()
	{
		foreach (PlayerScript playerScript in playerScripts)
		{
			playerScript.enabled = false;
		}
	}

}
