using System.Collections;
using UnityEngine;
using System.Linq;

public class SmokeContorller : MonoBehaviour
{
	ParticleSystem[] smokeParticles;

	Coroutine playRoutine;

	private void Awake()
	{
		smokeParticles = GetComponentsInChildren<ParticleSystem>();
	}

	private void OnEnable()
	{
		PlayAllSmoke();
	}

	private void OnDisable()
	{
		StopAllSmoke();
	}

	public void PlayAllSmoke()
	{
		playRoutine = StartCoroutine(PlaySmokeRoutine());
	}

	IEnumerator PlaySmokeRoutine()
	{
		for(int i= 0;i < smokeParticles.Length; i++)
		{
			smokeParticles[i].Play();

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void StopAllSmoke()
	{
		if(playRoutine != null)
		{
			StopCoroutine(playRoutine);
			playRoutine = null;
		}

		smokeParticles.ToList().ForEach(x => x.Stop());
	}

}
