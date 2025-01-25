using DubbelBubbel.Player;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	public static GameManager Instance => instance;

	public GameData gameData;

	private Player player;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			gameData = new GameData();
			gameData.OnPlayerDeath += GameData_OnPlayerDeath;
			player = Object.FindFirstObjectByType<Player>();
			DontDestroyOnLoad(gameObject);
		}
	}

	private void OnDestroy()
	{
		if(instance == this)
		{
			gameData.OnPlayerDeath -= GameData_OnPlayerDeath;
		}
	}

	private void GameData_OnPlayerDeath(object sender, System.EventArgs e)
	{
		if (!player.IsDestroyed())
		{
			Destroy(player.gameObject);
		}
	}
}
