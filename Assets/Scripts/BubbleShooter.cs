using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleShooter : MonoBehaviour
{
	InputAction attackAction;
	[SerializeField]
	private Bubble bubblePrefab;

	private void Awake()
	{
		attackAction = InputSystem.actions.FindAction("Attack");
		attackAction.performed += AttackAction_performed;
	}

	private void OnDestroy()
	{
		attackAction.performed -= AttackAction_performed;
	}

	private void AttackAction_performed(InputAction.CallbackContext obj)
	{
		if (GameManager.Instance.gameData.PowerUp >= 1) {
			if (GameManager.Instance.gameData.CanSpawnBubble())
			{
				var spawnedBubble = GameObject.Instantiate<Bubble>(bubblePrefab, transform.position + (transform.forward.normalized * 1.1f), transform.rotation);
				GameManager.Instance.gameData.SpawnBubble(spawnedBubble);

				spawnedBubble.BubbleLife = GameManager.Instance.gameData.BubbleTimer;
				spawnedBubble.BubbleDirection = transform.forward;
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
