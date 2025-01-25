using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLevelSwitcherInteraction : MonoBehaviour
{
    private InputAction interactAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
	{
		interactAction = InputSystem.actions.FindAction("Interact");
		interactAction.performed += InteractAction_performed;
	}

	private void InteractAction_performed(InputAction.CallbackContext obj)
	{
		var npc = GetLevelSwitcherNpcInRange();
		if (npc != null && GameManager.Instance.gameData.ItemsInWorld.Count == 0)
		{
			Debug.Log("NPC!!!");
			npc.LoadLevel();
		}
	}

	private LevelSwitcherNpc? GetLevelSwitcherNpcInRange()
	{
		var levelSwitcherNpcs = Object.FindObjectsByType<LevelSwitcherNpc>(FindObjectsSortMode.InstanceID);
		foreach (LevelSwitcherNpc levelSwitcherNpc in levelSwitcherNpcs)
		{
			if (levelSwitcherNpc.IsInRangeAndLooking(transform))
			{
				return levelSwitcherNpc;
			}
		}
		return null;
	}

	private void OnDestroy()
	{
		interactAction.performed -= InteractAction_performed;
	}
}
