using DubbelBubbel.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	public static GameManager Instance => instance;

	public GameData gameData;

	private bool playerDied = false;
	private float timeToEnd = 5;
	private float endTimer = 0;

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

	private void Update()
	{
		if (playerDied)
		{
			endTimer += Time.deltaTime;
			if(endTimer > timeToEnd)
			{
				ResetGame();

			}
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
		UIManager.instance.ShowText("It was fun and interesting to explore other people their bubbles. Let's do that again!");
		UIManager.instance.ShowGameOver();
	}

	public void ResetGame()
	{
		playerDied = false;
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
			UIManager.instance.ShowText("I didn't focus enough on other peoples bubbles. I guess I am too much in my own bubble");
			Destroy(player.gameObject);
			playerDied = true;
			endTimer = 0;

		}
	}

}
