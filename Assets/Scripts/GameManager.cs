using DubbelBubbel.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	public static GameManager Instance => instance;

	public GameData gameData;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			gameData = new GameData();
			DontDestroyOnLoad(gameObject);
		}
	}
	// called second
	void OnEnable()
	{
		gameData.OnPlayerDeath += GameData_OnPlayerDeath;
	}

	void OnDisable()
	{
		gameData.OnPlayerDeath -= GameData_OnPlayerDeath;
	}

	public void EndGame()
	{
		Debug.Log("End Game");
	}

	private void GameData_OnPlayerDeath(object sender, System.EventArgs e)
	{
		var player = Object.FindFirstObjectByType<Player>();
		if (player != null && !player.IsDestroyed())
		{
			Destroy(player.gameObject);
		}
	}
}
