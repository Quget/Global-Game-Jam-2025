using DubbelBubbel.Player;
using Unity.VisualScripting;
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

		private bool isInBubble = false;

		public void Awake()
		{
			rigedbody = GetComponent<Rigidbody>();
			playerTarget = FindFirstObjectByType<Player.Player>();
			movement = transform.position;
		}

		public void FixedUpdate()
		{
			if(isInBubble || playerTarget.IsDestroyed())
				return;

			if ((Vector3.Distance(transform.position, playerTarget.transform.position) < minDistance && !playerFound) ||
				Vector3.Distance(transform.position, playerTarget.transform.position) < minDistanceWhenPlayerIsFound && playerFound)
			{

				if (Physics.Raycast(transform.position + (transform.forward * 1.5f), Vector3.down, 2))
				{
					movement = transform.position + (transform.forward * speed * Time.fixedDeltaTime);
				}
				playerFound = true;
			}

			if (playerFound)
			{
				transform.LookAt(playerTarget.transform.position);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}
			rigedbody.MovePosition(movement);
		}

		public void OnCollisionEnter(Collision collision)
		{
				if (collision.collider.transform.parent.gameObject == playerTarget.gameObject)
			{
				if (isInBubble)
				{
					Destroy(this.gameObject);
				}
				else
				{
					playerTarget.GetComponent<Rigidbody>().AddForce((transform.forward.normalized + (Vector3.up * 0.25f)) * forceMultiplier);
					GameManager.Instance.gameData.LoseHealth();
					return;
				}
			}

			if (!isInBubble)
			{
				if (GameManager.Instance.gameData.PowerUp >= 2)
				{
					var bubble = collision.collider.GetComponentInParent<Bubble>();
					if (bubble != null)
					{
						isInBubble = true;
						rigedbody.isKinematic = true;
						bubble.Entrap(transform, () => {
							isInBubble = false;
							rigedbody.isKinematic = false;
						});
					}
				}
			}
		}
	}
}

