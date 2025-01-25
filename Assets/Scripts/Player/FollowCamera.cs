using DubbelBubbel.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

namespace DubbelBubbel.Player
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
        private GameObject targetObject;

        [SerializeField]
        private Vector3 positionOffset = new Vector3(1, 1, 1);

        [SerializeField]
        private Vector3 rotationOffset = new Vector3(1, 1, 1);

        [SerializeField]
        private float speed = 25f;

        [SerializeField]
        private float rotationSpeed = 25;

        private Vector3 currentVelocity = Vector3.zero;
        private InputAction lookAction;

        private Vector3 smoothedPosition;
        private Quaternion smoothedRotation;

        private Vector3 TargetPosition
        {
            get
            {
                return targetObject.transform.position;
            }
        }
        private Quaternion TargetRotation
        {
            get
            {
                return targetObject.transform.rotation;
            }
        }

        private void Awake()
        {
            lookAction = InputSystem.actions.FindAction("Look");
        }

		private void Update()
		{

			Vector3 desiredPosition = TargetPosition + TargetRotation * positionOffset;
			smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, speed * Time.deltaTime);

            //var lookVector = lookAction.ReadValue<Vector2>().normalized;
            //Todo do something with lookvector

            Quaternion desiredrotation = TargetRotation * Quaternion.Euler(rotationOffset);
			smoothedRotation = Quaternion.Lerp(transform.rotation, desiredrotation, rotationSpeed * Time.deltaTime);
		}

		private void FixedUpdate()
        {
			transform.position = smoothedPosition;
			transform.rotation = smoothedRotation;
        }
    }
}