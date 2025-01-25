using DubbelBubbel.Player;
using UnityEngine;

namespace DubbelBubbel.Enemies
{
	public class Bug : MonoBehaviour
	{
		[SerializeField]
		private float minDistance = 20;

		[SerializeField]
		private float minDistanceWhenPlayerIsFound = 50;

		[SerializeField]
		private float speed = 20;

		[SerializeField]
		private float forceMultiplier = 600;

		private new Rigidbody rigedbody;
		private Player.Player playerTarget;

		private Vector3 movement;

		private bool playerFound = false;

		public void Awake()
		{
			rigedbody = GetComponent<Rigidbody>();
			playerTarget = FindFirstObjectByType<Player.Player>();
			movement = transform.position;
		}

		public void Update()
		{
			if((Vector3.Distance(transform.position,playerTarget.transform.position) < minDistance && !playerFound) ||
				Vector3.Distance(transform.position, playerTarget.transform.position) < minDistanceWhenPlayerIsFound && playerFound)
			{
				movement = transform.position + (transform.forward  * speed * Time.deltaTime);
				playerFound = true;
			}
		}

		public void FixedUpdate()
		{
			if (playerFound)
			{
				transform.LookAt(playerTarget.transform.position);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}
			rigedbody.MovePosition(movement);
		}

		public void OnCollisionEnter(Collision collision)
		{
			if(collision.collider.gameObject == playerTarget.gameObject)
			{
				//Hurt player
				playerTarget.GetComponent<Rigidbody>().AddForce((transform.forward.normalized + (Vector3.up * 0.25f)) * forceMultiplier);
			}
		}
	}
}

