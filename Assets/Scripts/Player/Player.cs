using UnityEngine;


namespace DubbelBubbel.Player
{
	public class Player : MonoBehaviour
	{
		private Vector3 spawnPosition = Vector3.zero;

		private void Awake()
		{
			spawnPosition = transform.position;
		}

		private void Update()
		{
			if(transform.position.y == -20)
			{
				transform.position = spawnPosition;
			}
		}
	}
}