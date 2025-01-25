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
	}

	private void Update()
	{
		if (attackAction.IsPressed())
		{
			//spawn bubble
			if (GameManager.Instance.gameData.CanSpawnBubble())
			{
				var spawnedBubble = GameObject.Instantiate<Bubble>(bubblePrefab);
				GameManager.Instance.gameData.SpawnBubble(spawnedBubble);
				spawnedBubble.BubbleLife = GameManager.Instance.gameData.BubbleTimer;
				spawnedBubble.BubblePosition = transform.position;
				spawnedBubble.BubbleDirection = transform.position - (transform.position + transform.forward);
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
