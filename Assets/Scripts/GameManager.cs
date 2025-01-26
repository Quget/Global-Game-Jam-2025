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

		if (Instance != this)
		{
			DestroyImmediate(gameObject);
		}

	}
	void OnEnable()
	{
		if(instance == this)
		{
			gameData.OnPlayerDeath += GameData_OnPlayerDeath;
		}
		
	}

	void OnDisable()
	{
		if (instance == this)
		{
			gameData.OnPlayerDeath -= GameData_OnPlayerDeath;
		}
	}

	public void EndGame()
	{
		ResetGame();
	}

	private void ResetGame()
	{
		Instance.gameData.OnPlayerDeath -= GameData_OnPlayerDeath;
		Instance.gameData = new GameData();//reset;
		gameData.OnPlayerDeath += GameData_OnPlayerDeath;
		SceneManager.LoadScene(0);
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
