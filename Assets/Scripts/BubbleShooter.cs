using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleShooter : MonoBehaviour
{
	InputAction attackAction;

	[SerializeField]
	private Bubble bubblePrefab;

	[SerializeField]
	private AudioClip shootClip;

	private new Rigidbody rigidbody;

	private void Awake()
	{
		attackAction = InputSystem.actions.FindAction("Attack");
		attackAction.performed += AttackAction_performed;
		rigidbody = GetComponent<Rigidbody>();
	}

	private void OnDestroy()
	{
		attackAction.performed -= AttackAction_performed;
	}

	private void AttackAction_performed(InputAction.CallbackContext obj)
	{
		if (GameManager.Instance.gameData.PowerUp >= 1)
		{
			if (GameManager.Instance.gameData.CanSpawnBubble())
			{
				var spawnPosition = transform.position + (transform.forward.normalized * 2f);
				var spawnedBubble = GameObject.Instantiate<Bubble>(bubblePrefab, spawnPosition, transform.rotation);
				GameManager.Instance.gameData.SpawnBubble(spawnedBubble);

				spawnedBubble.BubbleLife = GameManager.Instance.gameData.BubbleTimer;
				spawnedBubble.BubbleDirection = transform.forward;

				AudioSource.PlayClipAtPoint(shootClip, spawnPosition);
			}
			for (int i = 0; i < GameManager.Instance.gameData.Bubbles.Count; i++)
			{
				if (GameManager.Instance.gameData.Bubbles[i].IsDestroyed())
				{
					GameManager.Instance.gameData.Bubbles.Remove(GameManager.Instance.gameData.Bubbles[i]);
				}
			}
		}
	}
}
