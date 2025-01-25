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
        private float rotationSpeed = 25;

        private Vector3 currentVelocity = Vector3.zero;
        private InputAction lookAction;

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
            if (Vector3.Distance(transform.position, TargetPosition) > 0)
            {
                Vector3 desiredPosition = TargetPosition + TargetRotation * positionOffset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 0.25f);
                transform.position = smoothedPosition;
            }

            var lookVector = lookAction.ReadValue<Vector2>().normalized;
            //rotationOffset += new Vector3(lookVector.y, lookVector.x);
            Quaternion desiredrotation = TargetRotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, 0.5f);
            transform.rotation = smoothedrotation;
        }
    }
}