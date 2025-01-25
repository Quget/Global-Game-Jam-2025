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
			var spawnedBubble = GameObject.Instantiate<Bubble>(bubblePrefab);
			GameManager.Instance.gameData.SpawnBubble(spawnedBubble);
		}
	}
}
