using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DubbelBubbel.Player
{
	public class PlayerMovement : MonoBehaviour
	{
		private new Rigidbody rigidbody;

		[SerializeField]
		private float speed = 100;

		[SerializeField]
		private float rotationSpeed = 100;

		[SerializeField]
		private float jumpForce = 200;

		[SerializeField]
		private AudioClip jumpClip;

		[SerializeField]
		private AudioClip jumpVoiceClip;

		[SerializeField]
		private AudioClip[] footSteps;

		[SerializeField]
		private AudioSource footSource;

		[SerializeField]
		private Animator animator;

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

		private void FixedUpdate()
		{
			MovementInput();
			MovementUpdate();
		}

		private void JumpAction_performed(InputAction.CallbackContext obj)
		{
			if (isOnGround)
			{
				rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				footSource.Stop();
				AudioSource.PlayClipAtPoint(jumpClip, transform.position);
				AudioSource.PlayClipAtPoint(jumpVoiceClip, transform.position);
				animator.SetTrigger("Jump");
			}
		}

		private void MovementInput()
		{
			var moveInput = moveAction.ReadValue<Vector2>().normalized;

			if(moveInput.magnitude > 0 && !footSource.isPlaying && isOnGround)
			{
				footSource.clip = GetRandomFootstepClip();
				footSource.Play();

			}

			if (moveInput.magnitude > 0 && isOnGround )
			{
				animator.SetTrigger("Walk");
			}
			else if(isOnGround && moveInput.magnitude == 0 /*&& animator.GetCurrentAnimatorClipInfo(0).FirstOrDefault().clip.name != "Idle"*/)
			{
				animator.SetTrigger("Idle");
			}
			//animator.SetBool("IsWalking", moveInput.magnitude > 0 && isOnGround);

			movement = transform.position + (transform.forward * moveInput.y * speed * Time.fixedDeltaTime);
			rotation = (moveInput.x * rotationSpeed) * Time.fixedDeltaTime;
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

		private AudioClip GetRandomFootstepClip()
		{
			return footSteps[Random.Range(0, footSteps.Length)];
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

