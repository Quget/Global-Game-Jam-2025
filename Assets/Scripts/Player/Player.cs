using UnityEngine;


namespace DubbelBubbel.Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField]
		private AudioClip hurtAudioClip;

		private new Rigidbody rigidbody;

		private Vector3 spawnPosition = Vector3.zero;

		private Quaternion spawnRotation = Quaternion.identity;

		private void Awake()
		{
			spawnPosition = transform.position;
			spawnRotation = transform.rotation;
			rigidbody = GetComponent<Rigidbody>();
		}

		public void DoDamage()
		{
			GameManager.Instance.gameData.LoseHealth();
			AudioSource.PlayClipAtPoint(hurtAudioClip, transform.position);
		}

		private void Update()
		{
			if(transform.position.y <= -20)
			{
				DoDamage();
				rigidbody.linearVelocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;

				transform.position = spawnPosition;
				rigidbody.position = spawnPosition;

				transform.rotation = spawnRotation;
				rigidbody.rotation = spawnRotation;
			}
		}
	}
}