using DubbelBubbel.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcherNpc : MonoBehaviour
{
	[SerializeField]
	private float distanceToBeInRange = 1;

	[SerializeField]
	private int levelToLoad = 0;

	private Player player;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>();
	}

	private void FixedUpdate()
	{
		if (player != null)
		{
			var distance = Vector3.Distance(player.transform.position, transform.position);
			if (distance < distanceToBeInRange)
			{
				transform.LookAt(player.transform.position);
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			}
		}
	}

	public bool IsInRangeAndLooking(Transform otherTransform)
	{
		var distance = Vector3.Distance(otherTransform.position, transform.position);
		if (distance < distanceToBeInRange)
		{
			Debug.Log(distance);
			float dot = Vector3.Dot(transform.forward, (otherTransform.position - transform.position).normalized);
			Debug.Log(dot);
			if (dot > 0.65f)
			{
				return true;
			}
		}

		return false;
	}
	public void LoadLevel()
	{
		//Check reqruirements and load next level!
		//Or show game over screen at the end?
		SceneManager.LoadScene(levelToLoad);

	}
}
