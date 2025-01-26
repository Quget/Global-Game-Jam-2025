using UnityEngine;

public class FootSteps : MonoBehaviour
{
	[SerializeField]
	private AudioClip[] footSteps;

	public void FootStep()
	{
		AudioSource.PlayClipAtPoint(GetRandomFootstepClip(), transform.position);
	}

	private AudioClip GetRandomFootstepClip()
	{
		return footSteps[Random.Range(0, footSteps.Length)];
	}
}
