using UnityEngine;

namespace DubbelBubbel.Player
{
	public class Player : MonoBehaviour
	{
		private new Rigidbody rigidbody;

		[SerializeField]
		private float speed = 100;

		[SerializeField]
		private float rotationSpeed = 100;

		[SerializeField]
		private float jumpForce = 200;

		private Vector3 movement = Vector3.zero;
		private float rotation = 0;

		private bool isOnGround = false;

		private void Awake()
		{
			rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			var horizontalInput = Input.GetAxis("Horizontal");
			var verticalInput = Input.GetAxis("Vertical");
			movement = transform.forward * verticalInput * speed;

			if (Input.GetButtonDown("Jump") && isOnGround)
			{
				rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z);
				rigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
				isOnGround = false;
			}
			rotation = horizontalInput * rotationSpeed;

		}

		private void LateUpdate()
		{
			Movement();
		}

		private void Movement()
		{
			rigidbody.linearVelocity = movement * Time.deltaTime;
			transform.Rotate(0, rotation * Time.deltaTime, 0);
		}

		private void OnCollisionStay(Collision collision)
		{
			if (!isOnGround)
			{
				for (int i = 0; i < collision.contacts.Length; i++)//Get all contact points
				{
					if (Vector3.Angle(collision.contacts[i].normal, Vector3.up) < 60)
					{
						isOnGround = true;
					}
				}
			}
		}

		private void OnCollisionExit(Collision collision)
		{
			isOnGround = false;
		}
	}
}

