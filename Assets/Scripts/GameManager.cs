using UnityEngine;

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
}
