using DubbelBubbel.Player;
using Unity.VisualScripting;
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
                if (targetObject.IsDestroyed())
                {
                    return FindFirstObjectByType<LevelSwitcherNpc>().transform.position;
                }
                return targetObject.transform.position;
            }
        }
        private Quaternion TargetRotation
        {
            get
            {
				if (targetObject.IsDestroyed())
				{
                    return FindFirstObjectByType<LevelSwitcherNpc>().transform.rotation;
				}
                return targetObject.transform.rotation;
            }
        }

        private void Awake()
        {
            lookAction = InputSystem.actions.FindAction("Look");
        }

		private void FixedUpdate()
		{
			Vector3 desiredPosition = TargetPosition + TargetRotation * positionOffset;
			smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, speed * Time.deltaTime);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = TargetRotation * Quaternion.Euler(rotationOffset);
			smoothedRotation = Quaternion.Lerp(transform.rotation, desiredrotation, rotationSpeed * Time.fixedDeltaTime);
			transform.rotation = smoothedRotation;
        }
    }
}