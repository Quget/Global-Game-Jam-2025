using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	public void ResetGame()
	{
		GameManager.Instance.ResetGame();
	}
}
