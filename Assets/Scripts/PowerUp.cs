using DubbelBubbel.Player;
using UnityEngine;

public class PowerUp : MonoBehaviour

{

    private Player player;

    void Awake()
    {
        player = FindFirstObjectByType<Player>();
        GameManager.Instance.gameData.PowerUpItems.Add(this);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.parent.gameObject == player.gameObject)
		{
            Debug.Log("Old PowerUp: " + GameManager.Instance.gameData.PowerUp);
            GameManager.Instance.gameData.PowerUpItems.Remove(this);
            GameManager.Instance.gameData.PickUpPowerUp();
            Destroy(this.gameObject);
            Debug.Log("New PowerUp: " + GameManager.Instance.gameData.PowerUp);
        }
	}
    
}
