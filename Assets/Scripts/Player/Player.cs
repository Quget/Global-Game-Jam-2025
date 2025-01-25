using UnityEngine;
using UnityEngine.InputSystem;

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

		InputAction moveAction;
		InputAction jumpAction;

		private void Awake()
		{
			moveAction = InputSystem.actions.FindAction("Move");
			jumpAction = InputSystem.actions.FindAction("Jump");
			jumpAction.performed += JumpAction_performed;

			rigidbody = GetComponent<Rigidbody>();
			movement = transform.position;
		}

		private void Update()
		{
			MovementInput();
		}

		private void FixedUpdate()
		{
			MovementUpdate();
		}

		private void JumpAction_performed(InputAction.CallbackContext obj)
		{
			if (isOnGround)
			{
				rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		private void MovementInput()
		{
			var moveInput = moveAction.ReadValue<Vector2>().normalized;
			movement = transform.position + (transform.forward * moveInput.y * speed * Time.deltaTime);
			rotation = (moveInput.x * rotationSpeed) * Time.deltaTime;
		}

		private void MovementUpdate()
		{
			rigidbody.MovePosition(movement);
			transform.Rotate(0, rotation, 0);
		}

		private void OnCollisionStay(Collision collision)
		{
			if (!isOnGround)
			{
				for (int i = 0; i < collision.contacts.Length; i++)
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

		private void OnDestroy()
		{
			jumpAction.performed -= JumpAction_performed;
		}
	}
}

