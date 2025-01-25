using Unity.VisualScripting;
using UnityEngine;

public class PlayerBubbleInteraction : MonoBehaviour
{
	[SerializeField]
	private float forceMultiplier = 100;

	private new Rigidbody rigidbody;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
	}


	private void OnCollisionEnter(Collision collision)
	{
		foreach(var bubble in GameManager.Instance.gameData.Bubbles)
		{
			if(!bubble.IsDestroyed() && collision.collider.transform.parent?.gameObject == bubble.gameObject)
			{
				Destroy(bubble.gameObject);
				rigidbody.AddForceAtPosition(Vector3.up * forceMultiplier, collision.contacts[0].point);
				break;
			}
		}
	}
}
