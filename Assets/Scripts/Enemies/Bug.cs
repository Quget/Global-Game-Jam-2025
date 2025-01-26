using DubbelBubbel.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace DubbelBubbel.Enemies
{
	public class Bug : MonoBehaviour
	{
		[SerializeField]
		private float minDistance = 10;

		[SerializeField]
		private float minDistanceWhenPlayerIsFound = 50;

		[SerializeField]
		private float speed = 3;

		[SerializeField]
		private float forceMultiplier = 200;

		[SerializeField]
		private GameObject bubbleEnclosure;

		[SerializeField]
		private AudioClip dieAudioClip;

		[SerializeField]
		private Animator animator;

		private new Rigidbody rigedbody;
		private Player.Player player;

		private Vector3 movement;

		private bool playerFound = false;

		private bool isInBubble = false;

		public void Awake()
		{
			rigedbody = GetComponent<Rigidbody>();
			player = FindFirstObjectByType<Player.Player>();
			movement = transform.position;
			UpdateBubbleEnclosure();
		}

		public void UpdateBubbleEnclosure()
		{
			bubbleEnclosure.SetActive(isInBubble);
		}
		public void FixedUpdate()
		{
			if(isInBubble || player.IsDestroyed())
			{
				animator.SetFloat("Walking", 0);
				return;
			}

			if ((Vector3.Distance(transform.position, player.transform.position) < minDistance && !playerFound) ||
				Vector3.Distance(transform.position, player.transform.position) < minDistanceWhenPlayerIsFound && playerFound)
			{

				if (Physics.Raycast(transform.position + (transform.forward * 1.5f), Vector3.down, 2))
				{
					movement = transform.position + (transform.forward * speed * Time.fixedDeltaTime);
				}
				playerFound = true;
			}

			if (playerFound)
			{
				transform.LookAt(player.transform.position);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}
			animator.SetFloat("Walking", movement.normalized.magnitude);
			rigedbody.MovePosition(movement);
		}

		public void OnCollisionEnter(Collision collision)
		{
			if (player != null && collision.collider.transform.gameObject == player.gameObject)
			{
				if (isInBubble)
				{
					for (int i = 0; i < collision.contacts.Length; i++)
					{
						Debug.Log(Vector3.Angle(collision.contacts[i].normal, Vector3.up));
						if (Vector3.Angle(collision.contacts[i].normal, Vector3.up) > 140)
						{
							Destroy(this.gameObject);
							break;
						}
					}
				}
				else
				{
					player.GetComponent<Rigidbody>().AddForce((transform.forward.normalized + (Vector3.up * 0.25f)) * forceMultiplier);
					player.DoDamage();
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
						UpdateBubbleEnclosure();
						bubble.Entrap(this.gameObject, () => {
							isInBubble = false;
							UpdateBubbleEnclosure();
						});
					}
				}
			}
		}

		private void OnDestroy()
		{
			AudioSource.PlayClipAtPoint(dieAudioClip, transform.position);
		}
	}
}

